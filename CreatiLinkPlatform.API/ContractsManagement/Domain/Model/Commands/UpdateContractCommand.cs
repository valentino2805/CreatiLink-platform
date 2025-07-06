namespace CreatiLinkPlatform.ContractsManagement.Domain.Model.Commands;

public record UpdateContractCommand(
    int ContractId,
decimal Price,
string Requirements,
string DesignType);