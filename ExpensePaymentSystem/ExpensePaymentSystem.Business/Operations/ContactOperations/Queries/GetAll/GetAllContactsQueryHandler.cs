using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.ContactOperations.Queries.GetAll;
public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, ApiResponse<List<ContactResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllContactsQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ContactResponse>>> Handle(GetAllContactsQuery request,
               CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Contact>()
            .Include(x => x.User)
            .Where(u => u.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Contact>, List<ContactResponse>>(list);
        return new ApiResponse<List<ContactResponse>>(mappedList);
    }

}