using CreatiLinkPlatform.API.Projects.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Projects.Domain.Model.Commands;
using CreatiLinkPlatform.API.Projects.Domain.Repositories;
using CreatiLinkPlatform.API.Projects.Domain.Services;
using CreatiLinkPlatform.API.Profile.Domain.Repositories;
using CreatiLinkPlatform.API.Shared.Domain.Repositories;

namespace CreatiLinkPlatform.API.Projects.Application.Internal.CommandServices;

public class ProjectCommandService(
    IProjectRepository projectRepository,
    IProfileRepository profileRepository, // nuevo
    IUnitOfWork unitOfWork)
    : IProjectCommandService
{
    public async Task<Project?> Handle(CreateProjectCommand command)
    {
        // ✅ Validación: ¿existe el perfil?
        var profileExists = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profileExists == null)
        {
            return null;
        }

        var project = new Project(
            command.ProfileId,
            command.Title,
            command.Image,
            command.Description,
            command.Technologies
        );

        await projectRepository.AddAsync(project);
        await unitOfWork.CompleteAsync();
        return project;
    }

    public async Task<Project?> Handle(UpdateProjectCommand command)
    {
        var project = await projectRepository.FindByIdAsync(command.Id);
        if (project == null) return null;

        project.UpdateProject(
            command.Title,
            command.Image,
            command.Description,
            command.Technologies
        );

        await unitOfWork.CompleteAsync();
        return project;
    }

    public async Task<bool> Handle(DeleteProjectCommand command)
    {
        var project = await projectRepository.FindByIdAsync(command.ProjectId);
        if (project == null) return false;

        projectRepository.Remove(project);
        await unitOfWork.CompleteAsync();
        return true;
    }
}