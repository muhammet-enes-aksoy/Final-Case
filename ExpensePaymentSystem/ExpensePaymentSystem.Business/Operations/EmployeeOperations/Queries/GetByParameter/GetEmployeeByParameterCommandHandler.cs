using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;

namespace ExpensePaymentSystem.Business.Operations.EmployeeOperations.Queries.GetByParameter;

public class GetEmployeeByParameterCommandHandler : IRequestHandler<GetEmployeeByParameterQuery, ApiResponse<List<EmployeeResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetEmployeeByParameterCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public async Task<ApiResponse<List<EmployeeResponse>>> Handle(GetEmployeeByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Employee>(true);
        if (!string.IsNullOrEmpty(request.FirstName))
            predicate = predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));

        if (!string.IsNullOrEmpty(request.LastName))
            predicate = predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));


        var list = await dbContext.Set<Employee>()
            .Include(x => x.Accounts)
            .Include(x => x.Addresses)
            .Include(x => x.Contacts)
            .Include(x => x.ExpenseClaims)
            .Where(u => u.IsActive)
            .Where(predicate).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Employee>, List<EmployeeResponse>>(list);
        return new ApiResponse<List<EmployeeResponse>>(mappedList);
    }
}