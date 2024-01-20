using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;


namespace ExpensePaymentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IMediator mediator;

    public CategoriesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<CategoryResponse>>> Get()
    {
        var operation = new GetAllCategoriesQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<CategoryResponse>> Get(int id)
    {
        var operation = new GetCategoryByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    public async Task<ApiResponse<List<CategoryResponse>>> GetByParameter(
        [FromQuery] string? CategoryType)
    {
        var operation = new GetCategoriesByParameterQuery(CategoryType);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<CategoryResponse>> Post([FromBody] CategoryRequest Category)
    {
        var operation = new CreateCategoryCommand(Category);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] CategoryRequest Category)
    {
        var operation = new UpdateCategoryCommand(id, Category);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteCategoryCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}