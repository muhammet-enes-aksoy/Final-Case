using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ExpensePaymentSystem.Business.Operations.ContactOperations.Commands.CreateContact;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ApiResponse<ContactResponse>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public CreateContactCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ContactResponse>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        bool isValidToAdd = request.Model.IsDefault
                    ? !await dbContext.Set<Contact>().AnyAsync(x => x.EmployeeId == request.Model.EmployeeId && x.IsDefault)
                    : true;

        if (!isValidToAdd)
            return new ApiResponse<ContactResponse>(string.Format(ContactMessages.DefaultContactAlreadyExistsForEmployeeId, request.Model.EmployeeId));

        var contact = mapper.Map<Contact>(request.Model);

        await dbContext.Contacts.AddAsync(contact, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ContactResponse>(contact);

        return new ApiResponse<ContactResponse>(response);
    }
}