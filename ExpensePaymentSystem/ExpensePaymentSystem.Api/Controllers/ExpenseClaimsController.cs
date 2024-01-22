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

    // Retrieve the expense claims associated with the authenticated employee.
    [HttpGet("MyExpenseClaims")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<ExpenseClaimResponse>> MyProfile()
    {
        // Extract user ID from claims.
        int id = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id")?.Value);

        // Create a query to get the expense claim by ID.
        var operation = new GetExpenseClaimByIdQuery(id);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve all expense claims (accessible only to Admin).
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseClaimResponse>>> Get()
    {
        // Create a query to get all expense claims.
        var operation = new GetAllExpenseClaimsQuery();

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve an expense claim by ID (accessible only to Admin).
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ExpenseClaimResponse>> Get(int id)
    {
        // Create a query to get the expense claim by ID.
        var operation = new GetExpenseClaimByIdQuery(id);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve admin expense claims based on parameters such as UserId and Status (accessible only to Admin).
    [HttpGet("ByParameters")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseClaimResponse>>> GetByParameter(
        [FromQuery] int UserId,
        [FromQuery] int Status)
    {
        // Create a query to get admin expense claims by parameters.
        var operation = new GetAdminExpenseClaimsByParameterQuery(UserId, Status);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve employee expense claims based on parameters such as Status and IsProcessed (accessible only to Employee).
    [HttpGet("GetExpenseClaimsByParameter")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<List<ExpenseClaimResponse>>> GetByParameter(
        [FromQuery] int Status,
        [FromQuery] bool IsProcessed)
    {
        // Extract user ID from claims.
        int id = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id")?.Value);

        // Create a query to get employee expense claims by parameters.
        var operation = new GetEmployeeExpenseClaimsByParameterQuery(id, Status, IsProcessed);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Create a new employee expense claim (accessible only to Employee).
    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<ExpenseClaimResponse>> Post([FromBody] EmployeeExpenseClaimRequest ExpenseClaim)
    {
        // Extract user ID from claims.
        int id = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id")?.Value);

        // Create a command to create a new employee expense claim.
        var operation = new CreateExpenseClaimCommand(id, ExpenseClaim);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Update an existing admin expense claim by ID (accessible only to Admin).
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] AdminExpenseClaimRequest ExpenseClaim)
    {
        // Create a command to update an existing admin expense claim.
        var operation = new UpdateAdminExpenseClaimCommand(id, ExpenseClaim);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Update an existing employee expense claim by ID (accessible only to Employee).
    [HttpPut("UpdateMyExpenseClaims")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse> Put(int id, [FromBody] EmployeeExpenseClaimRequest ExpenseClaim)
    {
        // Extract user ID from claims.
        int EmployeeId = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id")?.Value);

        // Create a command to update an existing employee expense claim.
        var operation = new UpdateEmployeeExpenseClaimCommand(EmployeeId, id, ExpenseClaim);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Delete an employee expense claim by ID (accessible only to Employee).
    [HttpDelete("{id}")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse> Delete(int id)
    {
        // Create a command to delete an employee expense claim.
        var operation = new DeleteExpenseClaimCommand(id);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }
}