using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.EmployeeOperations.Commands.DeleteEmployee;
public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand,ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public DeleteEmployeeCommandHandler(ExpensePaymentSystemDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Employee>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
        {
            return new ApiResponse(EmployeeMessages.RecordNotExists);
        }
        
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}