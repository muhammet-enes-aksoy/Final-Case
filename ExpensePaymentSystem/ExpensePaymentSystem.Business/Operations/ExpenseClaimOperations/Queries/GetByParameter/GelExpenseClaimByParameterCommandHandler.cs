using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.ExpenseClaimOperations.Queries.GetByParameter;

public class GetExpenseClaimByParameterQueryHandler : IRequestHandler<GetExpenseClaimsByParameterQuery, ApiResponse<List<ExpenseClaimResponse>>>
{

    private readonly ExpensePaymentSystemDbContext context;
    private readonly IMapper mapper;

    public GetExpenseClaimByParameterQueryHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseClaimResponse>>> Handle(GetExpenseClaimsByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<ExpenseClaim>(true);
        predicate.And(
            c => (request.UserId.Equals(0) || c.UserId.Equals(request.UserId)) &&
                 (request.ReceiptNumber == null || c.ReceiptNumber.ToUpper().Contains(request.ReceiptNumber.ToUpper())) &&
                 (request.Status == null || c.Status.ToUpper().Contains(request.Status.ToUpper())));

        var list = await context.Set<ExpenseClaim>()
            .Include(x => x.User)
            .Where(predicate).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<ExpenseClaim>, List<ExpenseClaimResponse>>(list);
        return new ApiResponse<List<ExpenseClaimResponse>>(mappedList);

    }
}