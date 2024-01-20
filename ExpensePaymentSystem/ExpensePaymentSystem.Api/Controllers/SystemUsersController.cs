using System.Security.Claims;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePaymentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SystemUsersController : ControllerBase
{
    private readonly IMediator mediator;

    public SystemUsersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    
    [HttpGet("MyProfile")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<SystemUserResponse>> MyProfile()
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetSystemUserByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<SystemUserResponse>>> Get()
    {
        var operation = new GetAllSystemUserQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<SystemUserResponse>> Get(int id)
    {
        var operation = new GetSystemUserByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<SystemUserResponse>>> GetByParameter(
        [FromQuery] string? FirstName,
        [FromQuery] string? LastName,
        [FromQuery] string? UserName)
    {
        var operation = new GetSystemUserByParameterQuery(FirstName,LastName,UserName);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<SystemUserResponse>> Post([FromBody] SystemUserRequest SystemUser)
    {
        var operation = new CreateSystemUserCommand(SystemUser);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] SystemUserRequest SystemUser)
    {
        var operation = new UpdateSystemUserCommand(id,SystemUser );
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteSystemUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}