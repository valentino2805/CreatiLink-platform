namespace CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Resources;

public record CreateContractResource(
    int UserId,
    int ProfileId,
    decimal Price,
    string Requirements,
    string DesignType
);