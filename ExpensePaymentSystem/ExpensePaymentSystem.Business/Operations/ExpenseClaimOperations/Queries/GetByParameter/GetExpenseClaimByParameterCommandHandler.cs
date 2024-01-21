using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.Schema.Enums;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.ExpenseClaimOperations.Queries.GetByParameter;
public class GetExpenseClaimByParameterQueryHandler : 
    IRequestHandler<GetAdminExpenseClaimsByParameterQuery, ApiResponse<List<ExpenseClaimResponse>>>,
    IRequestHandler<GetEmployeeExpenseClaimsByParameterQuery, ApiResponse<List<ExpenseClaimResponse>>>
{

    private readonly ExpensePaymentSystemDbContext context;
    private readonly IMapper mapper;

    public GetExpenseClaimByParameterQueryHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseClaimResponse>>> Handle(GetAdminExpenseClaimsByParameterQuery request, CancellationToken cancellationToken)
    {
        var tempStatus = ((ExpenseClaimStatus)request.Status).ToString();
        var predicate = PredicateBuilder.New<ExpenseClaim>(true);
        predicate.And(
            c => (request.EmployeeId.Equals(0) || c.EmployeeId.Equals(request.EmployeeId)) &&
                 (request.Status == 0 ||  c.Status.ToUpper().Contains(tempStatus.ToUpper())));

        var list = await context.Set<ExpenseClaim>()
            .Include(x => x.Employee)
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Where(predicate).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<ExpenseClaim>, List<ExpenseClaimResponse>>(list);
        return new ApiResponse<List<ExpenseClaimResponse>>(mappedList);
    }

    public async Task<ApiResponse<List<ExpenseClaimResponse>>> Handle(GetEmployeeExpenseClaimsByParameterQuery request, CancellationToken cancellationToken)
    {
        var tempStatus = ((ExpenseClaimStatus)request.Status).ToString();
        var predicate = PredicateBuilder.New<ExpenseClaim>(true);
        predicate.And(
            c => (request.EmployeeId.Equals(0) || c.EmployeeId.Equals(request.EmployeeId)) &&
                 (request.IsProcessed == null || c.IsProcessed.Equals(request.IsProcessed)) &&
                 (request.Status == 0 ||  c.Status.ToUpper().Contains(tempStatus.ToUpper())));
        
        var list = await context.Set<ExpenseClaim>()
            .Include(x => x.Employee)
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Where(predicate).ToListAsync(cancellationToken);

        foreach(var item in list){
            item.EmployeeId = request.EmployeeId;
        }

        var mappedList = mapper.Map<List<ExpenseClaim>, List<ExpenseClaimResponse>>(list);


        return new ApiResponse<List<ExpenseClaimResponse>>(mappedList);
    }
}