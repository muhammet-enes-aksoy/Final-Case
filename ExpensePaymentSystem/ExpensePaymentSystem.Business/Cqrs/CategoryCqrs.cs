using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Cqrs;
public record CreateCategoryCommand(CategoryRequest Model) : IRequest<ApiResponse<CategoryResponse>>;
public record UpdateCategoryCommand(int Id, CategoryRequest Model) : IRequest<ApiResponse>;
public record DeleteCategoryCommand(int Id) : IRequest<ApiResponse>;

public record GetAllCategoriesQuery() : IRequest<ApiResponse<List<CategoryResponse>>>;
public record GetCategoryByIdQuery(int Id) : IRequest<ApiResponse<CategoryResponse>>;
public record GetCategoriesByParameterQuery(string CategoryType) : IRequest<ApiResponse<List<CategoryResponse>>>;