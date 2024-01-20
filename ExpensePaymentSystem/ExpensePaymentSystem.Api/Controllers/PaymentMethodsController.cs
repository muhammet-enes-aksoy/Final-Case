using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;


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

    [HttpGet]
    public async Task<ApiResponse<List<PaymentMethodResponse>>> Get()
    {
        var operation = new GetAllPaymentMethodsQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<PaymentMethodResponse>> Get(int id)
    {
        var operation = new GetPaymentMethodByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    public async Task<ApiResponse<List<PaymentMethodResponse>>> GetByParameter(
        [FromQuery] string? PaymentMethodType)
    {
        var operation = new GetPaymentMethodsByParameterQuery(PaymentMethodType);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<PaymentMethodResponse>> Post([FromBody] PaymentMethodRequest PaymentMethod)
    {
        var operation = new CreatePaymentMethodCommand(PaymentMethod);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] PaymentMethodRequest PaymentMethod)
    {
        var operation = new UpdatePaymentMethodCommand(id, PaymentMethod);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeletePaymentMethodCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}