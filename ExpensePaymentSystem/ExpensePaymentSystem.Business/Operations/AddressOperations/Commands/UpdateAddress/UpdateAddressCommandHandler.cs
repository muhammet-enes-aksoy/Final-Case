using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.AddressOperations.Commands.UpdateAddress;
public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, ApiResponse>
{
     private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public UpdateAddressCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Address>().FindAsync(request.Id, cancellationToken);

        if (fromdb == null)
            return new ApiResponse(AddressMessages.RecordNotExists);

        var hasDefaultAddress = await dbContext.Set<Address>().AnyAsync(a => a.IsDefault && a.UserId.Equals(request.Id), cancellationToken);

        if (hasDefaultAddress && request.Model.IsDefault)
            return new ApiResponse(AddressMessages.CustomerAlreadyHasDefaultAddress);


        if (!(hasDefaultAddress || request.Model.IsDefault))
            fromdb.IsDefault = true;

        else
            fromdb.IsDefault = request.Model.IsDefault;

        fromdb.City = request.Model.City;
        fromdb.Country = request.Model.Country;
        fromdb.County = request.Model.County;
        fromdb.Address1 = request.Model.Address1;
        fromdb.Address2 = request.Model.Address2;
        fromdb.PostalCode = request.Model.PostalCode;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}