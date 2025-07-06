using CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create a ContractResource from a Contract entity
/// </summary>
public static class ContractResourceFromEntityAssembler
{
    /// <summary>
    /// Assembles a ContractResource from a Contract entity
    /// </summary>
    /// <param name="entity">
    /// The <see cref="Contract"/> entity to assemble the resource from
    /// </param>
    /// <returns>
    /// The <see cref="ContractResource"/> resource assembled from the entity
    /// </returns>
    public static ContractResource ToResourceFromEntity(Domain.Model.Aggregates.Contract entity)
    {
        return new ContractResource(
            entity.Id,
            entity.ClientUserId,
            entity.DesignerProfileId,
            entity.Price,
            entity.Requirements,
            entity.DesignType
        );
    }
}