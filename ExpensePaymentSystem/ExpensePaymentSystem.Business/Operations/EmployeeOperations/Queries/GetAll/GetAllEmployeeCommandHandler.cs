using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Operations.EmployeeOperations.Queries.GetAll;

public class GetAllEmployeeCommandHandler : IRequestHandler<GetAllEmployeeQuery, ApiResponse<List<EmployeeResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllEmployeeCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<EmployeeResponse>>> Handle(GetAllEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Employee>()
        .Include(x => x.Accounts)
        .Include(x => x.Addresses)
        .Include(x => x.Contacts)
        .Include(x => x.ExpenseClaims)
        .Where(u => u.IsActive)
        .ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Employee>, List<EmployeeResponse>>(list);
         return new ApiResponse<List<EmployeeResponse>>(mappedList);
    }

}