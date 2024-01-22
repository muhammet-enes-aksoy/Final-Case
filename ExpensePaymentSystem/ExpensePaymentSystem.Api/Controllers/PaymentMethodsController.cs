using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;
using Microsoft.AspNetCore.Authorization;


namespace ExpensePaymentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentMethodsController : ControllerBase
{
    private readonly IMediator mediator;

    public PaymentMethodsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    // Retrieve all payment methods (accessible only to Admin).
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<PaymentMethodResponse>>> Get()
    {
        // Create a query to get all payment methods.
        var operation = new GetAllPaymentMethodsQuery();

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve a payment method by ID (accessible only to Admin).
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<PaymentMethodResponse>> Get(int id)
    {
        // Create a query to get the payment method by ID.
        var operation = new GetPaymentMethodByIdQuery(id);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve payment methods based on parameters such as PaymentMethodType (accessible only to Admin).
    [HttpGet("ByParameters")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<PaymentMethodResponse>>> GetByParameter(
        [FromQuery] string? PaymentMethodType)
    {
        // Create a query to get payment methods by parameters.
        var operation = new GetPaymentMethodsByParameterQuery(PaymentMethodType);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Create a new payment method (accessible only to Admin).
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<PaymentMethodResponse>> Post([FromBody] PaymentMethodRequest PaymentMethod)
    {
        // Create a command to create a new payment method.
        var operation = new CreatePaymentMethodCommand(PaymentMethod);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Update an existing payment method by ID (accessible only to Admin).
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] PaymentMethodRequest PaymentMethod)
    {
        // Create a command to update an existing payment method.
        var operation = new UpdatePaymentMethodCommand(id, PaymentMethod);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Delete a payment method by ID (accessible only to Admin).
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        // Create a command to delete a payment method.
        var operation = new DeletePaymentMethodCommand(id);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }
}