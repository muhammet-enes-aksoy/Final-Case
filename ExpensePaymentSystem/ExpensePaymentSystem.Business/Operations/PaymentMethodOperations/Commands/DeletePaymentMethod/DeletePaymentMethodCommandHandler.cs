using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Schema;
using MediatR;


namespace ExpensePaymentSystem.Business.Operations.PaymentMethodOperations.Commands.DeletePaymentMethod;
public class DeletePaymentMethodCommandHandler : IRequestHandler<DeletePaymentMethodCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext context;
    private readonly IMapper mapper;

    public DeletePaymentMethodCommandHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var PaymentMethod = await context.PaymentMethods
            .FindAsync(request.Id, cancellationToken);

        if (PaymentMethod == null)
            return new ApiResponse(PaymentMethodMessages.RecordNotExists);

        PaymentMethod.IsActive = false;
        await context.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}