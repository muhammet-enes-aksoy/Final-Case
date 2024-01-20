using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.CategoryOperations.Queries.GetByParameter;

public class GetCategoryByParameterQueryHandler : IRequestHandler<GetCategoriesByParameterQuery, ApiResponse<List<CategoryResponse>>>
{

    private readonly ExpensePaymentSystemDbContext context;
    private readonly IMapper mapper;

    public GetCategoryByParameterQueryHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CategoryResponse>>> Handle(GetCategoriesByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Category>(true);
        predicate.And(c => request.CategoryType == null || c.CategoryType.ToUpper().Contains(request.CategoryType.ToUpper()));

        var list = await context.Set<Category>()
            .Where(x => x.IsActive)
            .Where(predicate).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Category>, List<CategoryResponse>>(list);
        return new ApiResponse<List<CategoryResponse>>(mappedList);

    }

}