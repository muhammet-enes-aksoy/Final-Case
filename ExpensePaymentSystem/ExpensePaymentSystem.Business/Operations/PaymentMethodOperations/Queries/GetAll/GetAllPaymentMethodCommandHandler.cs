using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.PaymentMethodOperations.Queries.GetAll;
public class GetAllPaymentMethodsQueryHandler : IRequestHandler<GetAllPaymentMethodsQuery, ApiResponse<List<PaymentMethodResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllPaymentMethodsQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentMethodResponse>>> Handle(GetAllPaymentMethodsQuery request,
               CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<PaymentMethod>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<PaymentMethod>, List<PaymentMethodResponse>>(list);
        return new ApiResponse<List<PaymentMethodResponse>>(mappedList);
    }

}