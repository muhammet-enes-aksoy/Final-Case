using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.PaymentMethodOperations.Queries.GetByParameter;

public class GetPaymentMethodByParameterQueryHandler : IRequestHandler<GetPaymentMethodsByParameterQuery, ApiResponse<List<PaymentMethodResponse>>>
{

    private readonly ExpensePaymentSystemDbContext context;
    private readonly IMapper mapper;

    public GetPaymentMethodByParameterQueryHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentMethodResponse>>> Handle(GetPaymentMethodsByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<PaymentMethod>(true);
        predicate.And(c => request.PaymentMethodType == null || c.PaymentMethodType.ToUpper().Contains(request.PaymentMethodType.ToUpper()));

        var list = await context.Set<PaymentMethod>()
            .Where(predicate).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<PaymentMethod>, List<PaymentMethodResponse>>(list);
        return new ApiResponse<List<PaymentMethodResponse>>(mappedList);

    }
}