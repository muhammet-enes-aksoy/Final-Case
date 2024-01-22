using System.Data.Entity;
using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using MediatR;


namespace ExpensePaymentSystem.Business.Operations.AccountOperations.Commands.DeleteAccount;
public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public DeleteAccountCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Account>().Where(x => x.AccountNumber == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (entity == null)
            return new ApiResponse(AccountMessages.RecordNotExists);
        
        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Account deleted!");
    }
}