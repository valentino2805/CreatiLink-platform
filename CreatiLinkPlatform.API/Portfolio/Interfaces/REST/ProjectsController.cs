using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CreatiLinkPlatform.API.Projects.Domain.Model.Commands;
using CreatiLinkPlatform.API.Projects.Domain.Model.Queries;
using CreatiLinkPlatform.API.Projects.Domain.Services;
using CreatiLinkPlatform.API.Projects.Interfaces.REST.Resources;
using CreatiLinkPlatform.API.Projects.Interfaces.REST.Transform;

namespace CreatiLinkPlatform.API.Projects.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Projects Endpoints.")]
public class ProjectsController(
    IProjectCommandService projectCommandService,
    IProjectQueryService projectQueryService
) : ControllerBase
{
    [HttpGet("{projectId:int}")]
    [SwaggerOperation(
        Summary = "Get project by id",
        Description = "Get project by id",
        OperationId = "GetProjectById"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The project with the given id", typeof(ProjectResource))]
    public async Task<IActionResult> GetProjectById([FromRoute] int projectId)
    {
        var project = await projectQueryService.Handle(new GetProjectByIdQuery(projectId));
        if (project is null) return NotFound();
        var projectResource = ProjectResourceFromEntityAssembler.ToResourceFromEntity(project);
        return Ok(projectResource);
    }
    
    [HttpGet("by-profile/{profileId:int}")]
    [SwaggerOperation(
        Summary = "Get all projects by profile id",
        Description = "Gets all the projects associated with a specific profile id",
        OperationId = "GetProjectsByProfileId"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "List of projects for given profile", typeof(IEnumerable<ProjectResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No projects found for the given profile id")]
    public async Task<IActionResult> GetProjectsByProfileId([FromRoute] int profileId)
    {
        var projects = await projectQueryService.Handle(new GetProjectsByProfileIdQuery(profileId));
        if (projects is null || !projects.Any()) return NotFound();
    
        var resources = projects.Select(ProjectResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }


    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new project",
        Description = "Create a new project",
        OperationId = "CreateProject"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The project created", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The project could not be created")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectResource resource)
    {
        var createCommand = CreateProjectCommandFromResourceAssembler.ToCommandFromResource(resource);
        var project = await projectCommandService.Handle(createCommand);
        if (project is null) return BadRequest();
        var projectResource = ProjectResourceFromEntityAssembler.ToResourceFromEntity(project);
        return CreatedAtAction(nameof(GetProjectById), new { projectId = project.Id }, projectResource);
    }

    [HttpPut("{projectId:int}")]
    [SwaggerOperation(
        Summary = "Update an existing project",
        Description = "Update an existing project",
        OperationId = "UpdateProject"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The project updated", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Project not found")]
    public async Task<IActionResult> UpdateProject([FromRoute] int projectId, [FromBody] UpdateProjectResource resource)
    {
        var updateCommand = UpdateProjectCommandFromResourceAssembler.ToCommandFromResource(projectId, resource);
        var project = await projectCommandService.Handle(updateCommand);
        if (project is null) return NotFound();
        var projectResource = ProjectResourceFromEntityAssembler.ToResourceFromEntity(project);
        return Ok(projectResource);
    }

    [HttpDelete("{projectId:int}")]
    [SwaggerOperation(
        Summary = "Delete a project",
        Description = "Delete a project",
        OperationId = "DeleteProject"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Project not found")]
    public async Task<IActionResult> DeleteProject([FromRoute] int projectId)
    {
        var isDeleted = await projectCommandService.Handle(new DeleteProjectCommand(projectId));
        if (!isDeleted) return NotFound();
        return NoContent();
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all projects",
        Description = "Get all projects",
        OperationId = "GetAllProjects"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "All projects", typeof(IEnumerable<ProjectResource>))]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await projectQueryService.Handle(new GetAllProjectsQuery());
        var resources = projects.Select(ProjectResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
