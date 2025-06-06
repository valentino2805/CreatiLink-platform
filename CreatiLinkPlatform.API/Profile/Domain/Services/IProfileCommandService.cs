using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Profile.Domain.Model.Commands;



namespace CreatiLinkPlatform.API.Profile.Domain.Services;

public interface IProfileCommandService
{
    Task<CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates.Profile?> Handle(CreateProfileCommand command);
    Task<CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates.Profile?> Handle(UpdateProfileCommand command);
    //Task Handle(DeleteProfileCommand command);
}