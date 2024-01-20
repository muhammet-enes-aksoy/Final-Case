using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.AccountOperations.Queries.GetById;
public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, ApiResponse<AccountResponse>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAccountByIdQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByIdQuery request,
               CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Account>()
            .Where(x => x.IsActive)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<AccountResponse>(AccountMessages.RecordNotExists);
        }
        
        var mapped = mapper.Map<Account, AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);
    }

    
}