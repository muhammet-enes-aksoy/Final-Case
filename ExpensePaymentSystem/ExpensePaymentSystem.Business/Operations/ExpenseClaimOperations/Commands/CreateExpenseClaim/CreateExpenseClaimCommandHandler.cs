using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.ExpenseClaimOperations.Commands.CreateExpenseClaim;

public class CreateExpenseClaimCommandHandler : IRequestHandler<CreateExpenseClaimCommand, ApiResponse<ExpenseClaimResponse>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public CreateExpenseClaimCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseClaimResponse>> Handle(CreateExpenseClaimCommand request, CancellationToken cancellationToken)
    {
        bool isValidToAdd = request.Model.IsDefault
                    ? !(await dbContext.Set<ExpenseClaim>().AnyAsync(x => x.UserId == request.Model.UserId && x.IsDefault))
                    : true;

        if (!isValidToAdd)
            return new ApiResponse<ExpenseClaimResponse>(string.Format(ExpenseClaimMessages.DefaultExpenseClaimAlreadyExistsForUserId, request.Model.UserId));

        var ExpenseClaim = mapper.Map<ExpenseClaim>(request.Model);

        await dbContext.ExpenseClaims.AddAsync(ExpenseClaim, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ExpenseClaimResponse>(ExpenseClaim);

        return new ApiResponse<ExpenseClaimResponse>(response);
    }
}