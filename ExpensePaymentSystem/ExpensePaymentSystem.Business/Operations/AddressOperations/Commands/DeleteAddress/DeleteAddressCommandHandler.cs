using System.Data.Entity;
using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using MediatR;

namespace ExpensePaymentSystem.Business.Operations.AddressOperations.Commands.DeleteAddress;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public DeleteAddressCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
       var fromdb = await dbContext.Set<Address>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (fromdb == null)
            return new ApiResponse(AddressMessages.RecordNotExists);

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Address deleted!");
    }
}
