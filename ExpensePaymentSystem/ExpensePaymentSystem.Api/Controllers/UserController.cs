using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;


namespace ExpensePaymentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator mediator;

    public UsersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    
    /*[HttpGet("MyProfile")]
    public async Task<ApiResponse<UserResponse>> MyProfile()
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetUserByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }*/
    
    [HttpGet]
    public async Task<ApiResponse<List<UserResponse>>> Get()
    {
        var operation = new GetAllUserQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<UserResponse>> Get(int id)
    {
        var operation = new GetUserByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    public async Task<ApiResponse<List<UserResponse>>> GetByParameter(
        [FromQuery] string? FirstName,
        [FromQuery] string? LastName)
    {
        var operation = new GetUserByParameterQuery(FirstName,LastName);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest User)
    {
        var operation = new CreateUserCommand(User);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UserRequest User)
    {
        var operation = new UpdateUserCommand(id,User );
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}