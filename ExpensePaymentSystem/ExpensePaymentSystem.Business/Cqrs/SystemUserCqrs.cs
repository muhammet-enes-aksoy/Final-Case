using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Cqrs;

public record CreateSystemUserCommand(SystemUserRequest Model) : IRequest<ApiResponse<SystemUserResponse>>;
public record UpdateSystemUserCommand(int Id,SystemUserRequest Model) : IRequest<ApiResponse>;
public record DeleteSystemUserCommand(int Id) : IRequest<ApiResponse>;

public record GetAllSystemUserQuery() : IRequest<ApiResponse<List<SystemUserResponse>>>;
public record GetSystemUserByIdQuery(int Id) : IRequest<ApiResponse<SystemUserResponse>>;
public record GetSystemUserByParameterQuery(string FirstName,string LastName,string UserName) : IRequest<ApiResponse<List<SystemUserResponse>>>;