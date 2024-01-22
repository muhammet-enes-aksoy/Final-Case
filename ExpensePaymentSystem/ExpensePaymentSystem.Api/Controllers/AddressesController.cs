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

    [HttpGet("MyAddresses")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<AddressResponse>> MyProfile()
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetAddressByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<AddressResponse>>> Get()
    {
        var operation = new GetAllAddressesQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<AddressResponse>> Get(int id)
    {
        var operation = new GetAddressByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<AddressResponse>>> GetByParameter(
        [FromQuery] int UserId,
        [FromQuery] string? County,
        [FromQuery] string? PostalCode)
    {
        var operation = new GetAddressByParameterQuery(UserId,County,PostalCode);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "Employee, Admin")]
    public async Task<ApiResponse<AddressResponse>> Post([FromBody] AddressRequest Address)
    {
        var operation = new CreateAddressCommand(Address);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Employee, Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] AddressRequest Address)
    {
        var operation = new UpdateAddressCommand(id, Address);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteAddressCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}