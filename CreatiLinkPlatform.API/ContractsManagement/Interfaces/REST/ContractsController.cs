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
/// <param name="contractCommandService">
/// The contract command service
/// </param>
/// <param name="contractQueryService">
/// The contract query service
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Contracts")]
public class ContractsController(
    IContractCommandService contractCommandService,
    IContractQueryService contractQueryService
    ) : ControllerBase
{
    /// <summary>
    /// Create a new contract (User only)
    /// </summary>
    /// <param name="resource">
    /// The <see cref="CreateContractResource"/> resource to create
    /// </param>
    /// <returns>
    /// The <see cref="ContractResource"/> resource created
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new contract",
        Description = "Create a new contract in the system (User only)",
        OperationId = "CreateContract")]
    [SwaggerResponse(StatusCodes.Status201Created, "The contract was created", typeof(ContractResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The contract was not created")]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractResource resource)
    {
        var createContractCommand = CreateContractCommandResourceFromEntityAssembler.ToCommandFromResource(resource);
        var contract = await contractCommandService.Handle(createContractCommand);
        if (contract is null) return BadRequest("Failed to create the contract. Please check the provided data.");
        var contractResource = ContractResourceFromEntityAssembler.ToResourceFromEntity(contract);
        return CreatedAtAction(nameof(GetContractById), new { contractId = contract.Id }, contractResource);
    }

    /// <summary>
    /// Update a contract (User and Profile)
    /// </summary>
    /// <param name="contractId">
    /// The id of the contract to update
    /// </param>
    /// <param name="resource">
    /// The <see cref="UpdateContractResource"/> resource to update
    /// </param>
    /// <returns>
    /// The updated <see cref="ContractResource"/> resource
    /// </returns>
    [HttpPut("{contractId:int}")]
    [SwaggerOperation(
        Summary = "Update a contract",
        Description = "Update a contract in the system (User and Profile)",
        OperationId = "UpdateContract")]
    [SwaggerResponse(StatusCodes.Status200OK, "The contract was updated", typeof(ContractResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The contract was not found")]
    public async Task<IActionResult> UpdateContract([FromRoute] int contractId, [FromBody] UpdateContractResource resource)
    {
        var updateContractCommand = UpdateContractCommandResourceFromEntityAssembler.ToCommandFromResource(contractId, resource);
        var contract = await contractCommandService.Handle(updateContractCommand);
        if (contract is null) return NotFound("The contract was not found.");
        var contractResource = ContractResourceFromEntityAssembler.ToResourceFromEntity(contract);
        return Ok(contractResource);
    }

    /// <summary>
    /// Delete a contract (User and Profile)
    /// </summary>
    /// <param name="contractId">
    /// The id of the contract to delete
    /// </param>
    /// <returns>
    /// A message indicating the contract was deleted
    /// </returns>
    [HttpDelete("{contractId:int}")]
    [SwaggerOperation(
        Summary = "Delete a contract",
        Description = "Delete a contract in the system (User and Profile)",
        OperationId = "DeleteContractById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The contract was deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The contract was not found")]
    public async Task<IActionResult> DeleteContractById([FromRoute] int contractId)
    {
        var deleteContractCommand = new DeleteContractCommand(contractId);
        await contractCommandService.Handle(deleteContractCommand);
        return Ok("The contract with the given id was successfully deleted.");
    }

    /// <summary>
    /// Get contract by id
    /// </summary>
    /// <param name="contractId">
    /// The id of the contract to get
    /// </param>
    /// <returns>
    /// The <see cref="ContractResource"/> resource with the given id
    /// </returns>
    [HttpGet("{contractId:int}")]
    [SwaggerOperation(
        Summary = "Get contract by id",
        Description = "Get a contract by the id it has",
        OperationId = "GetContractById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The contract was found", typeof(ContractResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The contract was not found")]
    public async Task<IActionResult> GetContractById([FromRoute] int contractId)
    {
        var getContractByIdQuery = new GetContractByIdQuery(contractId);
        var contract = await contractQueryService.Handle(getContractByIdQuery);
        if (contract is null) return NotFound();
        var contractResource = ContractResourceFromEntityAssembler.ToResourceFromEntity(contract);
        return Ok(contractResource);
    }

    /// <summary>
    /// Get contracts by user id
    /// </summary>
    /// <param name="userId">
    /// The id of the user to get contracts for
    /// </param>
    /// <returns>
    /// The <see cref="ContractResource"/> resources for the given user id
    /// </returns>
    [HttpGet("user/{userId:int}")]
    [SwaggerOperation(
        Summary = "Get contracts by user id",
        Description = "Get the contracts a user has",
        OperationId = "GetContractsByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The contracts were found", typeof(IEnumerable<ContractResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The contracts were not found")]
    public async Task<IActionResult> GetContractsByUserId([FromRoute] int userId)
    {
        var getAllContractsByUserIdQuery = new GetAllContractsByUserIdQuery(userId);
        var contracts = await contractQueryService.Handle(getAllContractsByUserIdQuery);
        var contractResources = contracts.Select(ContractResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(contractResources);
    }
}