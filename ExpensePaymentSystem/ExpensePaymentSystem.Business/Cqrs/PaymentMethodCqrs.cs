using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Cqrs;
public record CreatePaymentMethodCommand(PaymentMethodRequest Model) : IRequest<ApiResponse<PaymentMethodResponse>>;
public record UpdatePaymentMethodCommand(int Id, PaymentMethodRequest Model) : IRequest<ApiResponse>;
public record DeletePaymentMethodCommand(int Id) : IRequest<ApiResponse>;

public record GetAllPaymentMethodsQuery() : IRequest<ApiResponse<List<PaymentMethodResponse>>>;
public record GetPaymentMethodByIdQuery(int Id) : IRequest<ApiResponse<PaymentMethodResponse>>;
public record GetPaymentMethodsByParameterQuery(string PaymentMethodType) : IRequest<ApiResponse<List<PaymentMethodResponse>>>;