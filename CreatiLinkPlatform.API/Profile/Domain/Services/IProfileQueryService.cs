using CreatiLinkPlatform.API.Profile.Domain.Model.Queries;
using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Profile.Domain.Model.Commands;



namespace CreatiLinkPlatform.API.Profile.Domain.Services;

public interface IProfileQueryService
{
    Task<CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates.Profile?> Handle(GetProfileByIdQuery query);
    Task<IEnumerable<CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates.Profile>> Handle(GetAllProfilesQuery query);
    Task<IEnumerable<Model.Aggregates.Profile>> Handle(GetProfileByNameQuery query);
    
    Task<Model.Aggregates.Profile?> Handle(GetProfileByUserIdQuery query);

    

}