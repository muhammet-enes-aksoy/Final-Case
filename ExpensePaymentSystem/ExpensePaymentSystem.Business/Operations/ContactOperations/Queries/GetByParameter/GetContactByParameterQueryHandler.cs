using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.ContactOperations.Queries.GetByParameter;

public class GetContactByParameterQueryHandler : IRequestHandler<GetContactsByParameterQuery, ApiResponse<List<ContactResponse>>>
{

    private readonly ExpensePaymentSystemDbContext context;
    private readonly IMapper mapper;

    public GetContactByParameterQueryHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ContactResponse>>> Handle(GetContactsByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Contact>(true);
        predicate.And(
            c => (request.UserId.Equals(0) || c.UserId.Equals(request.UserId)) &&
                 (request.ContactType == null || c.ContactType.ToUpper().Contains(request.ContactType.ToUpper())) &&
                 (request.Information == null || c.Information.ToUpper().Contains(request.Information.ToUpper())));

        var list = await context.Set<Contact>()
            .Include(x => x.User)
            .Where(predicate).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Contact>, List<ContactResponse>>(list);
        return new ApiResponse<List<ContactResponse>>(mappedList);

    }
}