using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.EmployeeOperations.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler :
    IRequestHandler<CreateEmployeeCommand, ApiResponse<EmployeeResponse>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public CreateEmployeeCommandHandler(ExpensePaymentSystemDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<EmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await dbContext.Set<Employee>().Where(x => x.FirstName == request.Model.FirstName)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity != null)
        {
            return new ApiResponse<EmployeeResponse>($"{request.Model.FirstName} is in use.");
        }
        
        var entity = mapper.Map<EmployeeRequest, Employee>(request.Model);
        
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Employee, EmployeeResponse>(entityResult.Entity);
        return new ApiResponse<EmployeeResponse>(mapped);
    }
}