using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.AddressOperations.Queries.GetByParameter;

public class GetAddressByParameterQueryHandler :
    IRequestHandler<GetAddressByParameterQuery, ApiResponse<List<AddressResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAddressByParameterQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AddressResponse>>> Handle(GetAddressByParameterQuery request,
               CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Address>()
            .AsNoTracking() // since the data is fetched for read only purposes
                            // as no tracking is used to improve performance
            .Where(x =>
                       (request.County == null || x.County.ToUpper().Contains(request.County.ToUpper())) &&
                       (request.PostalCode == null || x.PostalCode.ToUpper().Contains(request.PostalCode.ToUpper())) &&
                       (request.UserId == 0 || x.UserId.Equals(request.UserId)))
            // since customerId is value type; in case of an empty value, it will be equal to 0
            // if auto increment is used, it will never be 0 for new records
            // otherwise, there must be a constraint applied
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Address>, List<AddressResponse>>(list);
        return new ApiResponse<List<AddressResponse>>(mappedList);
    }
}