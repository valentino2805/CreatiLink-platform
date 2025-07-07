using System.Net.Mime;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Commands;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Queries;
using CreatiLinkPlatform.ContractsManagement.Domain.Services;
using CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Resources;
using CreatiLinkPlatform.ContractsManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
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

    public ContractsController(
        IContractCommandService contractCommandService,
        IContractQueryService contractQueryService)
    {
        _contractCommandService = contractCommandService;
        _contractQueryService = contractQueryService;
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
            return BadRequest("Request body is null or improperly formatted.");
        }

        try
        {
            var createContractCommand = CreateContractCommandResourceFromEntityAssembler.ToCommandFromResource(resource);
            var contract = await _contractCommandService.Handle(createContractCommand);
            if (contract == null)
            {
                return BadRequest("Failed to create the contract. Please check the provided data.");
            }

            var contractResource = ContractResourceFromEntityAssembler.ToResourceFromEntity(contract);
            return CreatedAtAction(nameof(GetContractById), new { contractId = contract.Id }, contractResource);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
    /// <summary>
    /// Get all contracts
    /// </summary>
    /// <returns>
    /// A list of <see cref="ContractResource"/> resources
    /// </returns>
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
            var contracts = await _contractQueryService.Handle(new GetAllContractsQuery());
            var contractResources = contracts.Select(ContractResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(contractResources);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
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
            return BadRequest("Request body is null or improperly formatted.");
        }

        try
        {
            var updateContractCommand = UpdateContractCommandResourceFromEntityAssembler.ToCommandFromResource(contractId, resource);
            var contract = await _contractCommandService.Handle(updateContractCommand);
            if (contract == null)
            {
                return NotFound("The contract was not found.");
            }

            var contractResource = ContractResourceFromEntityAssembler.ToResourceFromEntity(contract);
            return Ok(contractResource);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
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
            var deleteContractCommand = new DeleteContractCommand(contractId);
            await _contractCommandService.Handle(deleteContractCommand);
            return Ok("The contract with the given id was successfully deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
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
            var getContractByIdQuery = new GetContractByIdQuery(contractId);
            var contract = await _contractQueryService.Handle(getContractByIdQuery);
            if (contract == null)
            {
                return NotFound();
            }

            var contractResource = ContractResourceFromEntityAssembler.ToResourceFromEntity(contract);
            return Ok(contractResource);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
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
            var getAllContractsByUserIdQuery = new GetAllContractsByUserIdQuery(userId);
            var contracts = await _contractQueryService.Handle(getAllContractsByUserIdQuery);
            if (!contracts.Any())
            {
                return NotFound("No contracts found for the given user id.");
            }

            var contractResources = contracts.Select(ContractResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(contractResources);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}