using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Business.Cqrs;

namespace ExpensePaymentSystem.Business.Operations.AddressOperations.Queries.GetAll;
public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, ApiResponse<List<AddressResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllAddressesQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AddressResponse>>> Handle(GetAllAddressesQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Address>()
            .Where(x => x.IsActive)
            .Include(x => x.User)
            .AsNoTracking()
            .Include(x => x.User).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Address>, List<AddressResponse>>(list);
        return new ApiResponse<List<AddressResponse>>(mappedList);
    }
}