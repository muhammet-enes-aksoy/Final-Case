using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;


namespace ExpensePaymentSystem.Business.Operations.EmployeeOperations.Queries.GetById;
public class GetEmployeeByIdCommandHandler : IRequestHandler<GetEmployeeByIdQuery, ApiResponse<EmployeeResponse>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetEmployeeByIdCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<EmployeeResponse>> Handle(GetEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Employee>()
            .Include(x => x.Accounts)
            .Include(x => x.Addresses)
            .Include(x => x.Contacts)
            .Include(x => x.ExpenseClaims)   
            .Where(u => u.IsActive)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<EmployeeResponse>(EmployeeMessages.RecordNotExists);
        }
        
        var mapped = mapper.Map<Employee, EmployeeResponse>(entity);
        return new ApiResponse<EmployeeResponse>(mapped);
    }

}