using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePaymentSystem.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IMediator mediator;

    public AccountsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<AccountResponse>>> Get()
    {
        var operation = new GetAllAccountsQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<AccountResponse>> Get(int id)
    {
        var operation = new GetAccountByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    public async Task<ApiResponse<List<AccountResponse>>> GetByParameter(
        [FromQuery] int CustomerId,
        [FromQuery] string? IBAN)
    {
        var operation = new GetAccountsByParameterQuery(CustomerId, IBAN);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<AccountResponse>> Post([FromBody] AccountRequest Account)
    {
        var operation = new CreateAccountCommand(Account);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] AccountRequest Account)
    {
        var operation = new UpdateAccountCommand(id, Account);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteAccountCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}