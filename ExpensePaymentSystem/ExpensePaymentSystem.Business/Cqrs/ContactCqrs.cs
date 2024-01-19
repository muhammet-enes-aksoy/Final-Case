using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Cqrs;
public record CreateContactCommand(ContactRequest Model) : IRequest<ApiResponse<ContactResponse>>;
public record UpdateContactCommand(int Id,ContactRequest Model) : IRequest<ApiResponse>;
public record DeleteContactCommand(int Id) : IRequest<ApiResponse>;

public record GetAllContactQuery() : IRequest<ApiResponse<List<ContactResponse>>>;
public record GetContactByIdQuery(int Id) : IRequest<ApiResponse<ContactResponse>>;
public record GetContactByParameterQuery(string FirstName,string LastName,string IdentityNumber) : IRequest<ApiResponse<List<ContactResponse>>>;