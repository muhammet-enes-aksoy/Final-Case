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

    [HttpGet("MyContacts")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<ContactResponse>> MyProfile()
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetContactByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet]
    public async Task<ApiResponse<List<ContactResponse>>> Get()
    {
        var operation = new GetAllContactsQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<ContactResponse>> Get(int id)
    {
        var operation = new GetContactByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    public async Task<ApiResponse<List<ContactResponse>>> GetByParameter(
        [FromQuery] int UserId,
        [FromQuery] string? ContactType,
        [FromQuery] string? Information)
    {
        var operation = new GetContactsByParameterQuery(UserId,ContactType,Information);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<ContactResponse>> Post([FromBody] ContactRequest Contact)
    {
        var operation = new CreateContactCommand(Contact);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] ContactRequest Contact)
    {
        var operation = new UpdateContactCommand(id, Contact);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteContactCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}