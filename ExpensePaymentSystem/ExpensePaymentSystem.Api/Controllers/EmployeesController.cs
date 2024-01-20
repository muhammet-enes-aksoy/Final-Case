using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;


namespace ExpensePaymentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IMediator mediator;

    public EmployeesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    
    /*[HttpGet("MyProfile")]
    public async Task<ApiResponse<EmployeeResponse>> MyProfile()
    {
        string id = (Employee.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetEmployeeByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }*/
    
    [HttpGet]
    public async Task<ApiResponse<List<EmployeeResponse>>> Get()
    {
        var operation = new GetAllEmployeeQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<EmployeeResponse>> Get(int id)
    {
        var operation = new GetEmployeeByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    public async Task<ApiResponse<List<EmployeeResponse>>> GetByParameter(
        [FromQuery] string? FirstName,
        [FromQuery] string? LastName)
    {
        var operation = new GetEmployeeByParameterQuery(FirstName,LastName);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<EmployeeResponse>> Post([FromBody] EmployeeRequest Employee)
    {
        var operation = new CreateEmployeeCommand(Employee);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] EmployeeRequest Employee)
    {
        var operation = new UpdateEmployeeCommand(id,Employee );
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteEmployeeCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}