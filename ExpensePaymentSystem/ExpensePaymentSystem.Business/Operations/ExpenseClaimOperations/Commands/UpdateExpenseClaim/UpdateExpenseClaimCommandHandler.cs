using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.ExpenseClaimOperations.Commands.UpdateExpenseClaim;
public class UpdateExpenseClaimCommandHandler : IRequestHandler<UpdateExpenseClaimCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public UpdateExpenseClaimCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdateExpenseClaimCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseClaim>().FindAsync(request.Id, cancellationToken);

        if (fromdb == null)
            return new ApiResponse(ExpenseClaimMessages.RecordNotExists);

        var hasDefaultExpenseClaim = await dbContext.Set<ExpenseClaim>().AnyAsync(c => c.IsDefault && c.EmployeeId == request.Id, cancellationToken);
        // true; if the customer already has a default ExpenseClaim

        if (hasDefaultExpenseClaim && request.Model.IsDefault)
            return new ApiResponse(string.Format(ExpenseClaimMessages.DefaultExpenseClaimAlreadyExistsForEmployeeId, request.Id));
        // if the customer already has a default ExpenseClaim and the request model is default,
        // just returns a message

        fromdb.Status = request.Model.Status;
        fromdb.StatusDescription = request.Model.StatusDescription;

        if (!(hasDefaultExpenseClaim || request.Model.IsDefault))
            fromdb.IsDefault = true;
        // if the customer doesn't have a default ExpenseClaim and the request model is not default,
        // set the current ExpenseClaim as default
        // this is to ensure that there is always a default ExpenseClaim for a customer        

        else
            fromdb.IsDefault = request.Model.IsDefault;

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ExpenseClaimResponse>(fromdb);

        return new ApiResponse();
    }

}