using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VbApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IMediator mediator;

    public TokenController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    // Authenticate and generate a token based on the provided credentials.
    [HttpPost("Authentication")]
    public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
    {
        // Create a command to authenticate and generate a token.
        var operation = new CreateTokenCommand(request);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }
}