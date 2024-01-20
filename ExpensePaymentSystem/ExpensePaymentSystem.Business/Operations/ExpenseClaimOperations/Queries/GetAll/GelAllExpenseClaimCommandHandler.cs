using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.ExpenseClaimOperations.Queries.GetAll;
public class GetAllExpenseClaimsQueryHandler : IRequestHandler<GetAllExpenseClaimsQuery, ApiResponse<List<ExpenseClaimResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllExpenseClaimsQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseClaimResponse>>> Handle(GetAllExpenseClaimsQuery request,
               CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ExpenseClaim>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<ExpenseClaim>, List<ExpenseClaimResponse>>(list);
        return new ApiResponse<List<ExpenseClaimResponse>>(mappedList);
    }

}