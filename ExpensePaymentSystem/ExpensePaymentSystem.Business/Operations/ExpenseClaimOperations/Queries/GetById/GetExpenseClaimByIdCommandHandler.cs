using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.ExpenseClaimOperations.Queries.GetById;

public class GetExpenseClaimByIdQueryHandler : IRequestHandler<GetExpenseClaimByIdQuery, ApiResponse<ExpenseClaimResponse>>
{
    private readonly ExpensePaymentSystemDbContext context;
    private readonly IMapper mapper;

    public GetExpenseClaimByIdQueryHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseClaimResponse>> Handle(GetExpenseClaimByIdQuery request, CancellationToken cancellationToken)
    {
        var ExpenseClaim = await context.Set<ExpenseClaim>()
            .Include(x => x.Employee)
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .FirstOrDefaultAsync(x => x.EmployeeId == request.Id, cancellationToken);

        if (ExpenseClaim == null)
            return new ApiResponse<ExpenseClaimResponse>(ExpenseClaimMessages.RecordNotExists);

        var response = mapper.Map<ExpenseClaimResponse>(ExpenseClaim);

        return new ApiResponse<ExpenseClaimResponse>(response);
    }
}