using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.EmployeeOperations.Commands.UpdateEmployee;
public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand,ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public UpdateEmployeeCommandHandler(ExpensePaymentSystemDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Employee>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
        {
            return new ApiResponse(EmployeeMessages.RecordNotExists);
        }
        
        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        fromdb.Role = request.Model.Role;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

}