using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace ExpensePaymentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseClaimsController : ControllerBase
{
    private readonly IMediator mediator;

    public ExpenseClaimsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("MyExpenseClaims")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<ExpenseClaimResponse>> MyProfile()
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetExpenseClaimByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet]
    public async Task<ApiResponse<List<ExpenseClaimResponse>>> Get()
    {
        var operation = new GetAllExpenseClaimsQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<ExpenseClaimResponse>> Get(int id)
    {
        var operation = new GetExpenseClaimByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    public async Task<ApiResponse<List<ExpenseClaimResponse>>> GetByParameter(
        [FromQuery] int UserId,
        [FromQuery] string? ReceiptNumber,
        [FromQuery] string? Status)
    {
        var operation = new GetExpenseClaimsByParameterQuery(UserId,ReceiptNumber,Status);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<ExpenseClaimResponse>> Post([FromBody] ExpenseClaimRequest ExpenseClaim)
    {
        var operation = new CreateExpenseClaimCommand(ExpenseClaim);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] ExpenseClaimRequest ExpenseClaim)
    {
        var operation = new UpdateExpenseClaimCommand(id,ExpenseClaim );
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteExpenseClaimCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}