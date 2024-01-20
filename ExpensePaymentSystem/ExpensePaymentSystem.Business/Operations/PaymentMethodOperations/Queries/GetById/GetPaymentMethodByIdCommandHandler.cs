using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.PaymentMethodOperations.Queries.GetById;

public class GetPaymentMethodByIdQueryHandler : IRequestHandler<GetPaymentMethodByIdQuery, ApiResponse<PaymentMethodResponse>>
{
    private readonly ExpensePaymentSystemDbContext _context;
    private readonly IMapper _mapper;

    public GetPaymentMethodByIdQueryHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PaymentMethodResponse>> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var PaymentMethod = await _context.PaymentMethods
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (PaymentMethod == null)
            return new ApiResponse<PaymentMethodResponse>(PaymentMethodMessages.RecordNotExists);

        var response = _mapper.Map<PaymentMethodResponse>(PaymentMethod);

        return new ApiResponse<PaymentMethodResponse>(response);
    }
}