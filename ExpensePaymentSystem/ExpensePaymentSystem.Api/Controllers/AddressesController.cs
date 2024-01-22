using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace ExpensePaymentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddresssController : ControllerBase
{
    private readonly IMediator mediator;

    public AddresssController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    // Retrieve the addresses associated with the authenticated employee.
    [HttpGet("MyAddresses")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<AddressResponse>> MyProfile()
    {
        // Extract user ID from claims.
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        // Create a query to get the address by ID.
        var operation = new GetAddressByIdQuery(int.Parse(id));
        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve all addresses (accessible only to Admin).
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<AddressResponse>>> Get()
    {
        // Create a query to get all addresses.
        var operation = new GetAllAddressesQuery();

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve an address by ID (accessible only to Admin).
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<AddressResponse>> Get(int id)
    {
        // Create a query to get the address by ID.
        var operation = new GetAddressByIdQuery(id);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve addresses based on parameters such as UserId, County, and PostalCode (accessible only to Admin).
    [HttpGet("ByParameters")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<AddressResponse>>> GetByParameter(
        [FromQuery] int UserId,
        [FromQuery] string? County,
        [FromQuery] string? PostalCode)
    {
        // Create a query to get addresses by parameters.
        var operation = new GetAddressByParameterQuery(UserId, County, PostalCode);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Create a new address (accessible to Admin and Employee).
    [HttpPost]
    [Authorize(Roles = "Employee, Admin")]
    public async Task<ApiResponse<AddressResponse>> Post([FromBody] AddressRequest Address)
    {
        // Create a command to create a new address.
        var operation = new CreateAddressCommand(Address);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Update an existing address by ID (accessible to Admin and Employee).
    [HttpPut("{id}")]
    [Authorize(Roles = "Employee, Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] AddressRequest Address)
    {
        // Create a command to update an existing address.
        var operation = new UpdateAddressCommand(id, Address);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Delete an address by ID (accessible only to Admin).
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        // Create a command to delete an address.
        var operation = new DeleteAddressCommand(id);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }
}