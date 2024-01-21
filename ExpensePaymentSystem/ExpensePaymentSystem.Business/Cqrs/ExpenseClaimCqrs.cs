using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Cqrs;
public record CreateExpenseClaimCommand(int EmployeeId, EmployeeExpenseClaimRequest Model) : IRequest<ApiResponse<ExpenseClaimResponse>>;
public record UpdateEmployeeExpenseClaimCommand(int EmployeeId, int Id, EmployeeExpenseClaimRequest Model) : IRequest<ApiResponse>;
public record UpdateAdminExpenseClaimCommand(int Id, AdminExpenseClaimRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenseClaimCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenseClaimsQuery() : IRequest<ApiResponse<List<ExpenseClaimResponse>>>;
public record GetExpenseClaimByIdQuery(int Id) : IRequest<ApiResponse<ExpenseClaimResponse>>;
public record GetAdminExpenseClaimsByParameterQuery(int EmployeeId, string Status) : IRequest<ApiResponse<List<ExpenseClaimResponse>>>;
public record GetEmployeeExpenseClaimsByParameterQuery(int EmployeeId, string Status, bool IsProcessed) : IRequest<ApiResponse<List<ExpenseClaimResponse>>>;
