using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Schema;
using MediatR;


namespace ExpensePaymentSystem.Business.Operations.ContactOperations.Commands.DeleteContact;
public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext context;
    private readonly IMapper mapper;

    public DeleteContactCommandHandler(ExpensePaymentSystemDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await context.Contacts
            .FindAsync(request.Id, cancellationToken);

        if (contact == null)
            return new ApiResponse(ContactMessages.RecordNotExists);

        contact.IsActive = false;
        await context.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}