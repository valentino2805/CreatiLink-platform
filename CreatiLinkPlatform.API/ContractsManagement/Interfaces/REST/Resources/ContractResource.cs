namespace CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Resources;

public record ContractResource(
    int Id,
    int UserId,
    int ProfileId,
    decimal Price,
    string Requirements,
    string DesignType
);