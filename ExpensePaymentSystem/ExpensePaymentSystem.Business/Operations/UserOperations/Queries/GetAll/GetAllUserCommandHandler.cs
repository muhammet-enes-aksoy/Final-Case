using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Operations.UserOperations.Queries.GetAll;

public class GetAllUserCommandHandler : IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllUserCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<User>()
        .Include(x => x.Accounts)
        .Include(x => x.Addresses)
        .Include(x => x.Contacts)
        .Include(x => x.ExpenseClaims)
        .Where(u => u.IsActive)
        .ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<User>, List<UserResponse>>(list);
         return new ApiResponse<List<UserResponse>>(mappedList);
    }

}