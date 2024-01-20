using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Business.Constants;

namespace ExpensePaymentSystem.Business.Operations.AddressOperations.Queries.GetById;
public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, ApiResponse<AddressResponse>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAddressByIdQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AddressResponse>> Handle(GetAddressByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Address>()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return new ApiResponse<AddressResponse>(AddressMessages.RecordNotExists);

        var mapped = mapper.Map<Address, AddressResponse>(entity);
        return new ApiResponse<AddressResponse>(mapped);
        
    }
}