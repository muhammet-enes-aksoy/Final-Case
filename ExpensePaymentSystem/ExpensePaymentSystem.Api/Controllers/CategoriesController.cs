using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Schema;
using Microsoft.AspNetCore.Authorization;

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
    // Retrieve all categories (accessible to both Admin and Employee).
    [HttpGet]
    [Authorize(Roles = "Admin, Employee")]
    public async Task<ApiResponse<List<CategoryResponse>>> Get()
    {
        // Create a query to get all categories.
        var operation = new GetAllCategoriesQuery();

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve a category by ID (accessible only to Admin).
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<CategoryResponse>> Get(int id)
    {
        // Create a query to get the category by ID.
        var operation = new GetCategoryByIdQuery(id);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Retrieve categories based on parameters such as CategoryType (accessible only to Admin).
    [HttpGet("ByParameters")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<CategoryResponse>>> GetByParameter(
        [FromQuery] string? CategoryType)
    {
        // Create a query to get categories by parameters.
        var operation = new GetCategoriesByParameterQuery(CategoryType);

        // Execute the query using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Create a new category (accessible only to Admin).
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<CategoryResponse>> Post([FromBody] CategoryRequest Category)
    {
        // Create a command to create a new category.
        var operation = new CreateCategoryCommand(Category);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Update an existing category by ID (accessible only to Admin).
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] CategoryRequest Category)
    {
        // Create a command to update an existing category.
        var operation = new UpdateCategoryCommand(id, Category);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }

    // Delete a category by ID (accessible only to Admin).
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        // Create a command to delete a category.
        var operation = new DeleteCategoryCommand(id);

        // Execute the command using MediatR and return the result.
        var result = await mediator.Send(operation);
        return result;
    }
}