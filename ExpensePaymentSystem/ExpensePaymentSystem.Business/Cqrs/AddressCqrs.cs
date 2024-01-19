using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;


namespace ExpensePaymentSystem.Business.Cqrs;

public record CreateAddressCommand(AddressRequest Model) : IRequest<ApiResponse<AddressResponse>>;
public record UpdateAddressCommand(int Id,AddressRequest Model) : IRequest<ApiResponse>;
public record DeleteAddressCommand(int Id) : IRequest<ApiResponse>;

public record GetAllAddressQuery() : IRequest<ApiResponse<List<AddressResponse>>>;
public record GetAddressByIdQuery(int Id) : IRequest<ApiResponse<AddressResponse>>;
public record GetAddressByParameterQuery(string FirstName,string LastName,string IdentityNumber) : IRequest<ApiResponse<List<AddressResponse>>>;