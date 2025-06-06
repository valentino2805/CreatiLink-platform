namespace CreatiLinkPlatform.API.Profile.Application.Internal.QueryServices;
using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Profile.Domain.Model.Queries;
using CreatiLinkPlatform.API.Profile.Domain.Repositories;
using CreatiLinkPlatform.API.Profile.Domain.Services;

public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId);
    }

    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }
    
    public async Task<IEnumerable<Profile>> Handle(GetProfileByNameQuery query)
    {
        return await profileRepository.FindByNameAsync(query.Name);
    }
    
    public async Task<Profile?> Handle(GetProfileByUserIdQuery query)
    {
        return await profileRepository.FindByUserIdAsync(query.UserId);
    }


}