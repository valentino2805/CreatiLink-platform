using CreatiLinkPlatform.ContractsManagement.Domain.Model.Commands;
using CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create a CreateContractCommand from a CreateContractResource
/// </summary>
public static class CreateContractCommandResourceFromEntityAssembler
{
    /// <summary>
    /// Assembles a CreateContractCommand from a CreateContractResource
    /// </summary>
    /// <param name="resource">
    /// The <see cref="CreateContractResource"/> resource to assemble the command from
    /// </param>
    /// <returns>
    /// The <see cref="CreateContractCommand"/> command assembled from the resource
    /// </returns>
    public static CreateContractCommand ToCommandFromResource(CreateContractResource resource)
    {
        return new CreateContractCommand(
            resource.UserId,
            resource.ProfileId,
            resource.Price,
            resource.Requirements,
            resource.DesignType
        );
    }
}