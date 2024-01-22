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
public class EmployeesController : ControllerBase
{
    private readonly IMediator mediator;

    public EmployeesController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    // Retrieve the profile information of the authenticated employee or admin.
    [HttpGet("MyProfile")]
    [Authorize(Roles = "Employee, Admin")]
    public async Task<ApiResponse<EmployeeResponse>> MyProfile()
    {
        // Extract user ID from claims.
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;

        // Create a query to get the employee by ID.
        var operation = new GetEmployeeByIdQuery(int.Parse(id));

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve all employees (accessible only to Admin).
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<EmployeeResponse>>> Get()
    {
        // Create a query to get all employees.
        var operation = new GetAllEmployeeQuery();

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve an employee by ID (accessible only to Admin).
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<EmployeeResponse>> Get(int id)
    {
        // Create a query to get the employee by ID.
        var operation = new GetEmployeeByIdQuery(id);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve employees based on parameters such as FirstName and LastName (accessible only to Admin).
    [HttpGet("ByParameters")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<EmployeeResponse>>> GetByParameter(
        [FromQuery] string? FirstName,
        [FromQuery] string? LastName)
    {
        // Create a query to get employees by parameters.
        var operation = new GetEmployeeByParameterQuery(FirstName, LastName);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Create a new employee (accessible only to Admin).
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<EmployeeResponse>> Post([FromBody] EmployeeRequest Employee)
    {
        // Create a command to create a new employee.
        var operation = new CreateEmployeeCommand(Employee);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Update an existing employee by ID (accessible only to Admin).
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] EmployeeRequest Employee)
    {
        // Create a command to update an existing employee.
        var operation = new UpdateEmployeeCommand(id, Employee);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Update the profile information of the authenticated employee.
    [HttpPut("UpdateMyProfile")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse> Put([FromBody] EmployeeRequest Employee)
    {
        // Extract user ID from claims.
        int id = int.Parse((User.Identity as ClaimsIdentity).FindFirst("Id")?.Value);

        // Create a command to update the profile of the authenticated employee.
        var operation = new UpdateEmployeeCommand(id, Employee);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Delete an employee by ID (accessible only to Admin).
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        // Create a command to delete an employee.
        var operation = new DeleteEmployeeCommand(id);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }
}