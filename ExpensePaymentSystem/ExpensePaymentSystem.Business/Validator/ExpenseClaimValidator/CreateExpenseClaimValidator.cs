using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validator;
public class CreateExpenseClaimValidator : AbstractValidator<EmployeeExpenseClaimRequest>
{
    public CreateExpenseClaimValidator()
    {
        RuleFor(ec => ec.CategoryId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("CategoryId is required and must be greater than 0.");

        RuleFor(ec => ec.PaymentMethodId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("PaymentMethodId is required and must be greater than 0.");

        RuleFor(ec => ec.PaymentLocation)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("PaymentLocation is required and cannot exceed 50 characters.");

        RuleFor(ec => ec.ReceiptNumber)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("ReceiptNumber is required and cannot exceed 50 characters.");

        RuleFor(ec => ec.Amount)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Amount is required and must be greater than 0.");

    }
}