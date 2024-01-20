using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Operations.ContactOperations.Queries.GetById;

public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ApiResponse<ContactResponse>>
{
    private readonly ExpensePaymentSystemDbContext _context;
    private readonly IMapper _mapper;

    public GetContactByIdQueryHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ContactResponse>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts
            .Include(x => x.Employee)
            .Where(u => u.IsActive)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (contact == null)
            return new ApiResponse<ContactResponse>(ContactMessages.RecordNotExists);

        var response = _mapper.Map<ContactResponse>(contact);

        return new ApiResponse<ContactResponse>(response);
    }
}