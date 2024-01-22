using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Schema;
using MediatR;


namespace ExpensePaymentSystem.Business.Operations.ExpenseClaimOperations.Commands.DeleteExpenseClaim;
public class DeleteExpenseClaimCommandHandler : IRequestHandler<DeleteExpenseClaimCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext context;
    private readonly IMapper mapper;

    public DeleteExpenseClaimCommandHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteExpenseClaimCommand request, CancellationToken cancellationToken)
    {
        var ExpenseClaim = await context.ExpenseClaims
            .FindAsync(request.Id, cancellationToken);

        if (ExpenseClaim == null)
            return new ApiResponse(ExpenseClaimMessages.RecordNotExists);

        ExpenseClaim.IsActive = false;
        await context.SaveChangesAsync(cancellationToken);

        return new ApiResponse("Expense claim deleted!");
    }
}