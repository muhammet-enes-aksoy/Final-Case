using FluentValidation;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.Business.Validator
{
    // Validator class for PaymentMethodRequest
    public class CreatePaymentMethodValidator : AbstractValidator<PaymentMethodRequest>
    {
        public CreatePaymentMethodValidator()
        {
            // PaymentMethodType is required and should have a maximum length of 50 characters
            RuleFor(x => x.PaymentMethodType).NotEmpty().MaximumLength(50).WithMessage("PaymentMethodType is required and should have a maximum length of 50 characters");
        }
    }
}
