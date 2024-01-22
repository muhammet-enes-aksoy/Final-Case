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
public class AccountsController : ControllerBase
{
    private readonly IMediator mediator;

    public AccountsController(IMediator mediator)
    {
        this.mediator = mediator;
    }
 // Retrieve the accounts associated with the authenticated user (Employee or Admin).
    [HttpGet("MyAccounts")]
    [Authorize(Roles = "Employee, Admin")]
    public async Task<ApiResponse<AccountResponse>> MyProfile()
    {
        // Extract user ID from claims.
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        // Create a query to get the account by ID.
        var operation = new GetAccountByIdQuery(int.Parse(id));
        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }
     // Retrieve all accounts (accessible only to Admin).
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<AccountResponse>>> Get()
    {
        // Create a query to get all accounts.
        var operation = new GetAllAccountsQuery();
        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve an account by ID (accessible only to Admin).
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<AccountResponse>> Get(int id)
    {
        // Create a query to get the account by ID.
        var operation = new GetAccountByIdQuery(id);
        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve accounts based on parameters such as CustomerId and IBAN (accessible only to Admin).
    [HttpGet("ByParameters")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<AccountResponse>>> GetByParameter(
        [FromQuery] int CustomerId,
        [FromQuery] string? IBAN)
    {
        // Create a query to get accounts by parameters.
        var operation = new GetAccountsByParameterQuery(CustomerId, IBAN);
        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Create a new account (accessible to Admin and Employee).
    [HttpPost]
    [Authorize(Roles = "Admin, Employee")]
    public async Task<ApiResponse<AccountResponse>> Post([FromBody] AccountRequest Account)
    {
        // Create a command to create a new account.
        var operation = new CreateAccountCommand(Account);
        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Update an existing account by ID (accessible only to Admin).
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] AccountRequest Account)
    {
         // Create a command to update an existing account.
        var operation = new UpdateAccountCommand(id, Account);
        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Delete an account by ID (accessible only to Admin).
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        // Create a command to delete an account.
        var operation = new DeleteAccountCommand(id);
         // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }
}