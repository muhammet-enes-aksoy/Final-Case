using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace ExpensePaymentSystem.Business.Operations.AddressOperations.Commands.CreateAddress;
public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ApiResponse<AddressResponse>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public CreateAddressCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        bool isValidToAdd = request.Model.IsDefault
                ? !(await dbContext.Set<Address>().AnyAsync(addr => addr.UserId == request.Model.UserId && addr.IsDefault))
                : true;

        if (!isValidToAdd)
            return new ApiResponse<AddressResponse>(string.Format(AddressMessages.DefaultAddressAlreadyExistsForCustomerId, request.Model.Id));

        var entity = mapper.Map<Address>(request.Model);

        await dbContext.Set<Address>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Address, AddressResponse>(entity);
        return new ApiResponse<AddressResponse>(mapped);

    }
}