using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.ExpenseClaimOperations.Queries.GetById;

public class GetExpenseClaimByIdQueryHandler : IRequestHandler<GetExpenseClaimByIdQuery, ApiResponse<ExpenseClaimResponse>>
{
    private readonly ExpensePaymentSystemDbContext _context;
    private readonly IMapper _mapper;

    public GetExpenseClaimByIdQueryHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseClaimResponse>> Handle(GetExpenseClaimByIdQuery request, CancellationToken cancellationToken)
    {
        var ExpenseClaim = await _context.ExpenseClaims
            .Include(x => x.User)
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (ExpenseClaim == null)
            return new ApiResponse<ExpenseClaimResponse>(ExpenseClaimMessages.RecordNotExists);

        var response = _mapper.Map<ExpenseClaimResponse>(ExpenseClaim);

        return new ApiResponse<ExpenseClaimResponse>(response);
    }
}