using System.Net.Mime;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Commands;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Queries;
using CreatiLinkPlatform.ContractsManagement.Domain.Services;
using CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Resources;
using CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace CreatiLinkPlatform.ContractsManagement.Interfaces.REST;

/// <summary>
/// Controller for managing contracts
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Contracts")]
public class ContractsController : ControllerBase
{
    private readonly IContractCommandService _contractCommandService;
    private readonly IContractQueryService _contractQueryService;
    private readonly ILogger<ContractsController> _logger;

    public ContractsController(
        IContractCommandService contractCommandService,
        IContractQueryService contractQueryService,
        ILogger<ContractsController> logger)
    {
        _contractCommandService = contractCommandService;
        _contractQueryService = contractQueryService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new contract (User only)
    /// </summary>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new contract",
        Description = "Create a new contract in the system (User only)",
        OperationId = "CreateContract")]
    [SwaggerResponse(StatusCodes.Status201Created, "The contract was created", typeof(ContractResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The contract was not created")]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractResource resource)
    {
        if (resource == null)
        {
            _logger.LogWarning("CreateContract: Request body is null or improperly formatted.");
            return BadRequest("Request body is null or improperly formatted.");
        }

        try
        {
            _logger.LogInformation("CreateContract: Processing request to create a new contract.");
            var createContractCommand = CreateContractCommandResourceFromEntityAssembler.ToCommandFromResource(resource);
            var contract = await _contractCommandService.Handle(createContractCommand);
            if (contract == null)
            {
                _logger.LogWarning("CreateContract: Failed to create the contract. Provided data may be invalid.");
                return BadRequest("Failed to create the contract. Please check the provided data.");
            }

            var contractResource = ContractResourceFromEntityAssembler.ToResourceFromEntity(contract);
            _logger.LogInformation("CreateContract: Contract created successfully with ID {ContractId}.", contract.Id);
            return CreatedAtAction(nameof(GetContractById), new { contractId = contract.Id }, contractResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateContract: An error occurred while processing the request.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Get all contracts
    /// </summary>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all contracts",
        Description = "Retrieve all contracts in the system",
        OperationId = "GetAllContracts")]
    [SwaggerResponse(StatusCodes.Status200OK, "The contracts were found", typeof(IEnumerable<ContractResource>))]
    public async Task<IActionResult> GetAllContracts()
    {
        try
        {
            _logger.LogInformation("GetAllContracts: Retrieving all contracts.");
            var contracts = await _contractQueryService.Handle(new GetAllContractsQuery());
            var contractResources = contracts.Select(ContractResourceFromEntityAssembler.ToResourceFromEntity);
            _logger.LogInformation("GetAllContracts: Successfully retrieved {Count} contracts.", contractResources.Count());
            return Ok(contractResources);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetAllContracts: An error occurred while retrieving contracts.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Update a contract (User and Profile)
    /// </summary>
    [HttpPut("{contractId:int}")]
    [SwaggerOperation(
        Summary = "Update a contract",
        Description = "Update a contract in the system (User and Profile)",
        OperationId = "UpdateContract")]
    [SwaggerResponse(StatusCodes.Status200OK, "The contract was updated", typeof(ContractResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The contract was not found")]
    public async Task<IActionResult> UpdateContract([FromRoute] int contractId, [FromBody] UpdateContractResource resource)
    {
        if (resource == null)
        {
            _logger.LogWarning("UpdateContract: Request body is null or improperly formatted.");
            return BadRequest("Request body is null or improperly formatted.");
        }

        try
        {
            _logger.LogInformation("UpdateContract: Processing request to update contract with ID {ContractId}.", contractId);
            var updateContractCommand = UpdateContractCommandResourceFromEntityAssembler.ToCommandFromResource(contractId, resource);
            var contract = await _contractCommandService.Handle(updateContractCommand);
            if (contract == null)
            {
                _logger.LogWarning("UpdateContract: Contract with ID {ContractId} not found.", contractId);
                return NotFound("The contract was not found.");
            }

            var contractResource = ContractResourceFromEntityAssembler.ToResourceFromEntity(contract);
            _logger.LogInformation("UpdateContract: Contract with ID {ContractId} updated successfully.", contractId);
            return Ok(contractResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateContract: An error occurred while updating contract with ID {ContractId}.", contractId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Delete a contract (User and Profile)
    /// </summary>
    [HttpDelete("{contractId:int}")]
    [SwaggerOperation(
        Summary = "Delete a contract",
        Description = "Delete a contract in the system (User and Profile)",
        OperationId = "DeleteContractById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The contract was deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The contract was not found")]
    public async Task<IActionResult> DeleteContractById([FromRoute] int contractId)
    {
        try
        {
            _logger.LogInformation("DeleteContractById: Processing request to delete contract with ID {ContractId}.", contractId);
            var deleteContractCommand = new DeleteContractCommand(contractId);
            await _contractCommandService.Handle(deleteContractCommand);
            _logger.LogInformation("DeleteContractById: Contract with ID {ContractId} deleted successfully.", contractId);
            return Ok("The contract with the given id was successfully deleted.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteContractById: An error occurred while deleting contract with ID {ContractId}.", contractId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Get contract by id
    /// </summary>
    [HttpGet("{contractId:int}")]
    [SwaggerOperation(
        Summary = "Get contract by id",
        Description = "Get a contract by the id it has",
        OperationId = "GetContractById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The contract was found", typeof(ContractResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The contract was not found")]
    public async Task<IActionResult> GetContractById([FromRoute] int contractId)
    {
        try
        {
            _logger.LogInformation("GetContractById: Retrieving contract with ID {ContractId}.", contractId);
            var getContractByIdQuery = new GetContractByIdQuery(contractId);
            var contract = await _contractQueryService.Handle(getContractByIdQuery);
            if (contract == null)
            {
                _logger.LogWarning("GetContractById: Contract with ID {ContractId} not found.", contractId);
                return NotFound();
            }

            var contractResource = ContractResourceFromEntityAssembler.ToResourceFromEntity(contract);
            _logger.LogInformation("GetContractById: Contract with ID {ContractId} retrieved successfully.", contractId);
            return Ok(contractResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetContractById: An error occurred while retrieving contract with ID {ContractId}.", contractId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Get contracts by user id
    /// </summary>
    [HttpGet("user/{userId:int}")]
    [SwaggerOperation(
        Summary = "Get contracts by user id",
        Description = "Get the contracts a user has",
        OperationId = "GetContractsByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The contracts were found", typeof(IEnumerable<ContractResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The contracts were not found")]
    public async Task<IActionResult> GetContractsByUserId([FromRoute] int userId)
    {
        try
        {
            _logger.LogInformation("GetContractsByUserId: Retrieving contracts for user ID {UserId}.", userId);
            var getAllContractsByUserIdQuery = new GetAllContractsByUserIdQuery(userId);
            var contracts = await _contractQueryService.Handle(getAllContractsByUserIdQuery);
            if (!contracts.Any())
            {
                _logger.LogWarning("GetContractsByUserId: No contracts found for user ID {UserId}.", userId);
                return NotFound("No contracts found for the given user id.");
            }

            var contractResources = contracts.Select(ContractResourceFromEntityAssembler.ToResourceFromEntity);
            _logger.LogInformation("GetContractsByUserId: Successfully retrieved {Count} contracts for user ID {UserId}.", contractResources.Count(), userId);
            return Ok(contractResources);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetContractsByUserId: An error occurred while retrieving contracts for user ID {UserId}.", userId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}