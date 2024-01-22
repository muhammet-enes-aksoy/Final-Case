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
public class ContactsController : ControllerBase
{
    private readonly IMediator mediator;

    public ContactsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    // Retrieve the contacts associated with the authenticated employee.
    [HttpGet("MyContacts")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<ContactResponse>> MyProfile()
    {
        // Extract user ID from claims.
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;

        // Create a query to get the contact by ID.
        var operation = new GetContactByIdQuery(int.Parse(id));

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve all contacts (accessible only to Admin).
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ContactResponse>>> Get()
    {
        // Create a query to get all contacts.
        var operation = new GetAllContactsQuery();

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve a contact by ID (accessible only to Admin).
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ContactResponse>> Get(int id)
    {
        // Create a query to get the contact by ID.
        var operation = new GetContactByIdQuery(id);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve contacts based on parameters such as UserId, ContactType, and Information (accessible only to Admin).
    [HttpGet("ByParameters")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ContactResponse>>> GetByParameter(
        [FromQuery] int UserId,
        [FromQuery] string? ContactType,
        [FromQuery] string? Information)
    {
        // Create a query to get contacts by parameters.
        var operation = new GetContactsByParameterQuery(UserId, ContactType, Information);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Create a new contact (accessible to both Admin and Employee).
    [HttpPost]
    [Authorize(Roles = "Employee, Admin")]
    public async Task<ApiResponse<ContactResponse>> Post([FromBody] ContactRequest Contact)
    {
        // Create a command to create a new contact.
        var operation = new CreateContactCommand(Contact);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Update an existing contact by ID (accessible to both Admin and Employee).
    [HttpPut("{id}")]
    [Authorize(Roles = "Employee, Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] ContactRequest Contact)
    {
        // Create a command to update an existing contact.
        var operation = new UpdateContactCommand(id, Contact);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Delete a contact by ID (accessible only to Admin).
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        // Create a command to delete a contact.
        var operation = new DeleteContactCommand(id);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }
}