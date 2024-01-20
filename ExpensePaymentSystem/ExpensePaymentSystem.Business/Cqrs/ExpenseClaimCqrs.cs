using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Cqrs;
public record CreateExpenseClaimCommand(ExpenseClaimRequest Model) : IRequest<ApiResponse<ExpenseClaimResponse>>;
public record UpdateExpenseClaimCommand(int Id, ExpenseClaimRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenseClaimCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenseClaimsQuery() : IRequest<ApiResponse<List<ExpenseClaimResponse>>>;
public record GetExpenseClaimByIdQuery(int Id) : IRequest<ApiResponse<ExpenseClaimResponse>>;
public record GetExpenseClaimsByParameterQuery(int EmployeeId, string ReceiptNumber, string Status) : IRequest<ApiResponse<List<ExpenseClaimResponse>>>;