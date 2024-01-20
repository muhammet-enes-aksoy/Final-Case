using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.PaymentMethodOperations.Commands.UpdatePaymentMethod;
public class UpdatePaymentMethodCommandHandler : IRequestHandler<UpdatePaymentMethodCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public UpdatePaymentMethodCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<PaymentMethod>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
            return new ApiResponse(PaymentMethodMessages.RecordNotExists);


        entity.PaymentMethodType = request.Model.PaymentMethodType;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

}