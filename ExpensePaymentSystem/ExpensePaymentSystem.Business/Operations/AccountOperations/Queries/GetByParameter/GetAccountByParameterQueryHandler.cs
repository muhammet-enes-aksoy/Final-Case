using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.AccountOperations.Queries.GetByParameter;
public class GetAccountsByParameterQueryHandler : IRequestHandler<GetAccountsByParameterQuery, ApiResponse<List<AccountResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;
    public GetAccountsByParameterQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAccountsByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Account>(true);
      
      predicate.And(acc => (request.CustomerId.Equals(0) || acc.UserId.Equals(request.CustomerId)) &&
                           (request.IBAN == null || acc.IBAN.ToUpper().Contains(request.IBAN.ToUpper())));

        var list =  await dbContext.Set<Account>()
            .Include(x => x.Name)
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Account>, List<AccountResponse>>(list);
        return new ApiResponse<List<AccountResponse>>(mappedList);
    }

}