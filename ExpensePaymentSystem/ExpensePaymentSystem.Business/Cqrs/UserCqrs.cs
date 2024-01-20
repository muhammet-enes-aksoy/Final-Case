using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Cqrs;
public record CreateUserCommand(UserRequest Model) : IRequest<ApiResponse<UserResponse>>;
public record UpdateUserCommand(int Id, UserRequest Model) : IRequest<ApiResponse>;
public record DeleteUserCommand(int Id) : IRequest<ApiResponse>;

public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;
public record GetUserByIdQuery(int Id) : IRequest<ApiResponse<UserResponse>>;
public record GetUserByParameterQuery(string FirstName,string LastName) : IRequest<ApiResponse<List<UserResponse>>>;