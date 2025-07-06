using CreatiLinkPlatform.ContractsManagement.Domain.Model.Commands;
using CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create an UpdateContractCommand from an UpdateContractResource
/// </summary>
public static class UpdateContractCommandResourceFromEntityAssembler
{
    /// <summary>
    /// Assembles an UpdateContractCommand from an UpdateContractResource
    /// </summary>
    /// <param name="contractId">
    /// The unique identifier of the contract to update
    /// </param>
    /// <param name="resource">
    /// The <see cref="UpdateContractResource"/> resource to assemble the command from
    /// </param>
    /// <returns>
    /// The <see cref="UpdateContractCommand"/> command assembled from the resource
    /// </returns>
    public static UpdateContractCommand ToCommandFromResource(int contractId, UpdateContractResource resource)
    {
        return new UpdateContractCommand(
            contractId,
            resource.Price,
            resource.Requirements,
            resource.DesignType
        );
    }
}