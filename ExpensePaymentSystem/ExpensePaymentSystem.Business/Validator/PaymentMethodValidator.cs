using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validator;
public class PaymentMethodValidator : AbstractValidator<PaymentMethodRequest>
{
    public PaymentMethodValidator()
    {
        RuleFor(c => c.PaymentMethodType).NotEmpty()
            .WithMessage(PaymentMethodMessages.PaymentMethodTypeIsRequired)
            .MaximumLength(100)
            .WithMessage(PaymentMethodMessages.PaymentMethodTypeMaxLength);
    }
}