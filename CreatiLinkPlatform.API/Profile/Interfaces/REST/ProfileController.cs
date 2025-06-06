using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CreatiLinkPlatform.API.Profile.Domain.Model.Commands;
using CreatiLinkPlatform.API.Profile.Domain.Model.Queries;
using CreatiLinkPlatform.API.Profile.Domain.Services;
using CreatiLinkPlatform.API.Profile.Interfaces.REST.Resources;
using CreatiLinkPlatform.API.Profile.Interfaces.REST.Transform;

namespace CreatiLinkPlatform.API.Profile.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Endpoints for managing user profiles.")]
public class ProfilesController(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService
) : ControllerBase
{
    [HttpGet("{profileId:int}")]
    [SwaggerOperation(
        Summary = "Get profile by id",
        Description = "Retrieves a user profile by its unique identifier",
        OperationId = "GetProfileById"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The profile with the given id", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> GetProfileById([FromRoute] int profileId)
    {
        var profile = await profileQueryService.Handle(new GetProfileByIdQuery(profileId));
        if (profile is null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResource(profile);
        return Ok(profileResource);
    }

    
    [HttpGet("search")]
    [SwaggerOperation(
        Summary = "Search profiles by name",
        Description = "Returns a list of profiles whose names match the given query",
        OperationId = "SearchProfilesByName"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "List of matching profiles", typeof(IEnumerable<ProfileResource>))]
    public async Task<IActionResult> GetProfilesByName([FromQuery] string name)
    {
        var query = new GetProfileByNameQuery(name);
        var profiles = await profileQueryService.Handle(query);
        var resources = profiles.Select(ProfileResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }
    
    [HttpGet("by-user")]
    public async Task<IActionResult> GetProfileByUserId([FromQuery] int userId)
    {
        var query = new GetProfileByUserIdQuery(userId);
        var profile = await profileQueryService.Handle(query);

        if (profile is null) return NotFound();

        var profileResource = ProfileResourceFromEntityAssembler.ToResource(profile);
        return Ok(profileResource);
    }

    

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all profiles",
        Description = "Retrieves all registered profiles",
        OperationId = "GetAllProfiles"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "All profiles", typeof(IEnumerable<ProfileResource>))]
    public async Task<IActionResult> GetAllProfiles()
    {
        var query = new GetAllProfilesQuery();
        var profiles = await profileQueryService.Handle(query);
        var resources = profiles.Select(ProfileResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new profile",
        Description = "Registers a new profile in the system",
        OperationId = "CreateProfile"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The profile created", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data or error creating the profile")]
    public async Task<IActionResult> CreateProfileTest([FromBody] CreateProfileResource resource)
    {
        // 🟡 Aceptamos el userId directamente del body para probar
        if (resource.UserId <= 0)
            return BadRequest("Invalid or missing UserId");

        var command = CreateProfileCommandFromResourceAssembler.ToCommand(resource, resource.UserId);

        var profile = await profileCommandService.Handle(command);
        if (profile is null) return BadRequest("User not found or could not create profile.");

        var profileResource = ProfileResourceFromEntityAssembler.ToResource(profile);
        return CreatedAtAction(nameof(GetProfileById), new { profileId = profile.Id }, profileResource);
    }

    [HttpPut("{profileId:int}")]
    [SwaggerOperation(
        Summary = "Update an existing profile",
        Description = "Updates profile information by its id",
        OperationId = "UpdateProfile"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The updated profile", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> UpdateProfile([FromRoute] int profileId, [FromBody] UpdateProfileResource resource)
    {
        var command = UpdateProfileCommandFromResourceAssembler.ToCommand(profileId, resource);
        var profile = await profileCommandService.Handle(command);
        if (profile is null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResource(profile);
        return Ok(profileResource);
    }
}
