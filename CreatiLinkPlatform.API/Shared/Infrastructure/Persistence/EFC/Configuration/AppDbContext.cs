using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

//using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Projects.Domain.Model.Aggregates;

namespace CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Profiles Context

        builder.Entity<Profile.Domain.Model.Aggregates.Profile>().HasKey(p => p.Id);
        builder.Entity<Profile.Domain.Model.Aggregates.Profile>().Property(p => p.Id).HasColumnName("profile_id")
            .IsRequired().ValueGeneratedOnAdd();

        builder.Entity<Profile.Domain.Model.Aggregates.Profile>().Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Profile.Domain.Model.Aggregates.Profile>().Property(p => p.Location).IsRequired()
            .HasMaxLength(150);
        builder.Entity<Profile.Domain.Model.Aggregates.Profile>().Property(p => p.Bio).IsRequired();
        builder.Entity<Profile.Domain.Model.Aggregates.Profile>().Property(p => p.Image).IsRequired();
        builder.Entity<Profile.Domain.Model.Aggregates.Profile>().Property(p => p.Icon).IsRequired();

        builder.Entity<Profile.Domain.Model.Aggregates.Profile>()
            .HasOne<Users>(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        var experienceConverter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
        );

        builder.Entity<Profile.Domain.Model.Aggregates.Profile>().Property(p => p.Experience)
            .HasConversion(experienceConverter)
            .HasColumnType("longtext")

            .IsRequired();

        builder.Entity<Profile.Domain.Model.Aggregates.Profile>().OwnsOne(
            p => p.Social,
            sa =>
            {
                sa.Property(p => p.Instagram).HasColumnName("instagram").HasMaxLength(200);
                sa.Property(p => p.Facebook).HasColumnName("facebook").HasMaxLength(200);
                sa.Property(p => p.X).HasColumnName("x").HasMaxLength(200);
                sa.WithOwner();
            });





        // IAM Context

        builder.Entity<Users>().ToTable("users");

        builder.Entity<Users>().HasKey(u => u.Id);
        builder.Entity<Users>().Property(u => u.Id).IsRequired();
        builder.Entity<Users>().Property(u => u.Email).IsRequired();
        builder.Entity<Users>().Property(u => u.Password).IsRequired();
        builder.Entity<Users>().Property(u => u.Role).IsRequired();
        builder.Entity<Users>().Property(u => u.ProfileId);

/*
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();

        builder.UseSnakeCaseNamingConvention();
        */

// Projects Context
        builder.Entity<Project>().ToTable("projects");

        builder.Entity<Project>().HasKey(p => p.Id);
        builder.Entity<Project>().Property(p => p.Id).HasColumnName("project_id").IsRequired().ValueGeneratedOnAdd();

        builder.Entity<Project>().Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Entity<Project>().Property(p => p.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<Project>().Property(p => p.Image).IsRequired().HasMaxLength(500);

        builder.Entity<Project>().Property(p => p.Likes).IsRequired();
        builder.Entity<Project>().Property(p => p.Comments).IsRequired();

        builder.Entity<Project>().Property(p => p.Technologies)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
            )
            .HasColumnType("longtext")
            .IsRequired();

        builder.Entity<Project>().Property(p => p.ProfileId).IsRequired();

// ✅ Relación correcta con Profile
        builder.Entity<Project>()
            .HasOne(p => p.Profile)
            .WithMany()
            .HasForeignKey(p => p.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
