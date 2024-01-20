using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.CategoryOperations.Queries.GetAll;
public class GetAllCategorysQueryHandler : IRequestHandler<GetAllCategoriesQuery, ApiResponse<List<CategoryResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllCategorysQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CategoryResponse>>> Handle(GetAllCategoriesQuery request,
               CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Category>()
        .Where(x => x.IsActive)
        .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Category>, List<CategoryResponse>>(list);
        return new ApiResponse<List<CategoryResponse>>(mappedList);
    }

}