using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CreatiLinkPlatform.API.Profile.Application.Internal.CommandServices;
using CreatiLinkPlatform.API.Profile.Application.Internal.QueryServices;
using CreatiLinkPlatform.API.Profile.Domain.Repositories;
using CreatiLinkPlatform.API.Profile.Domain.Services;

using CreatiLinkPlatform.API.Profile.Infrastructure.Repositories;
using CreatiLinkPlatform.API.Shared.Domain.Repositories;
using CreatiLinkPlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;


using CreatiLinkPlatform.API.IAM.Application.ACL.Services;
using CreatiLinkPlatform.API.IAM.Application.Internal.CommandServices;
using CreatiLinkPlatform.API.IAM.Application.Internal.OutboundServices;
using CreatiLinkPlatform.API.IAM.Application.Internal.QueryServices;
using CreatiLinkPlatform.API.IAM.Domain.Repositories;
using CreatiLinkPlatform.API.IAM.Domain.Services;
using CreatiLinkPlatform.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using CreatiLinkPlatform.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using CreatiLinkPlatform.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using CreatiLinkPlatform.API.IAM.Infrastructure.Tokens.JWT.Services;
using CreatiLinkPlatform.API.IAM.Interfaces.ACL;


using CreatiLinkPlatform.API.Projects.Application.Internal.CommandServices;
using CreatiLinkPlatform.API.Projects.Application.Internal.QueryServices;
using CreatiLinkPlatform.API.Projects.Application.Internal.QueryServices.acl;
using CreatiLinkPlatform.API.Projects.Domain.Repositories;
using CreatiLinkPlatform.API.Projects.Domain.Services;
using CreatiLinkPlatform.API.Projects.Infrastructure.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Apply Route Naming Convention
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader());
});

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString is null)
    throw new Exception("Connection string is null");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString);
});

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "CreatiLink Platform API",
            Version = "v1",
            Description = "CreatiLink Platform - Profiles Context",
            TermsOfService = new Uri("https://creatilink.dev"),
            Contact = new OpenApiContact
            {
                Name = "CreatiLink Support",
                Email = "support@creatilink.dev"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });

    options.EnableAnnotations();
});

// Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Shared bounded context dependency injection configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Profiles Bounded Context
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Projects Bounded Context

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectCommandService, ProjectCommandService>();
builder.Services.AddScoped<IProjectQueryService, ProjectQueryService>();





// TokenSettings configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

// Middleware
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
