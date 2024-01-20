using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.PaymentMethodOperations.Commands.CreatePaymentMethod;

public class CreatePaymentMethodCommandHandler :
    IRequestHandler<CreatePaymentMethodCommand, ApiResponse<PaymentMethodResponse>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public CreatePaymentMethodCommandHandler(ExpensePaymentSystemDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<PaymentMethodResponse>> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await dbContext.Set<PaymentMethod>()
        .FindAsync(request.Model.PaymentMethodType, cancellationToken);

        if (checkIdentity != null)
            return new ApiResponse<PaymentMethodResponse>(PaymentMethodMessages.PaymentMethodAlreadyExists);
        
        var entity = mapper.Map<PaymentMethodRequest, PaymentMethod>(request.Model);
        
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<PaymentMethod, PaymentMethodResponse>(entityResult.Entity);
        return new ApiResponse<PaymentMethodResponse>(mapped);
    }
}