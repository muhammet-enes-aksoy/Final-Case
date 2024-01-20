using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.AccountOperations.Queries.GetAll;
public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, ApiResponse<List<AccountResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllAccountsQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountsQuery request,
                   CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Account>()
            .Include(x => x.User).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Account>, List<AccountResponse>>(list);
         return new ApiResponse<List<AccountResponse>>(mappedList);

    }
}