using System.Data.Entity;
using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;

namespace ExpensePaymentSystem.Business.Operations.UserOperations.Queries.GetByParameter;

public class GetUserByParameterCommandHandler : IRequestHandler<GetUserByParameterQuery, ApiResponse<List<UserResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetUserByParameterCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public async Task<ApiResponse<List<UserResponse>>> Handle(GetUserByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<User>(true);
        if (string.IsNullOrEmpty(request.FirstName))
            predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
        if (string.IsNullOrEmpty(request.LastName))
            predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
        
        
        var list =  await dbContext.Set<User>()
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<User>, List<UserResponse>>(list);
        return new ApiResponse<List<UserResponse>>(mappedList);
    }
}