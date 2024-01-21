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
        int id = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id")?.Value);
        var operation = new GetExpenseClaimByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseClaimResponse>>> Get()
    {
        var operation = new GetAllExpenseClaimsQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ExpenseClaimResponse>> Get(int id)
    {
        var operation = new GetExpenseClaimByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseClaimResponse>>> GetByParameter(
        [FromQuery] int UserId,
        [FromQuery] int Status)
    {
        var operation = new GetAdminExpenseClaimsByParameterQuery(UserId,Status);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetExpenseClaimsByParameter")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<List<ExpenseClaimResponse>>> GetByParameter(
        [FromQuery] int Status,
        [FromQuery] bool IsProcessed)
    {
        int id = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id")?.Value);
        var operation = new GetEmployeeExpenseClaimsByParameterQuery(id,Status,IsProcessed);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<ExpenseClaimResponse>> Post([FromBody] EmployeeExpenseClaimRequest ExpenseClaim)
    {
        int id = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id")?.Value);
        var operation = new CreateExpenseClaimCommand(id, ExpenseClaim);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] AdminExpenseClaimRequest ExpenseClaim)
    {
        var operation = new UpdateAdminExpenseClaimCommand(id,ExpenseClaim );
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("UpdateMyExpenseClaims")]
    [Authorize(Roles = "Employee")]
     public async Task<ApiResponse> Put(int id, [FromBody] EmployeeExpenseClaimRequest ExpenseClaim)
    {
        int EmployeeId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id")?.Value);
        var operation = new UpdateEmployeeExpenseClaimCommand(EmployeeId,id,ExpenseClaim );
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteExpenseClaimCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}