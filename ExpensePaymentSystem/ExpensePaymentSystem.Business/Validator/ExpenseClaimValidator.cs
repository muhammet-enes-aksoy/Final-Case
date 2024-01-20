using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validator;
public class ExpenseClaimValidator : AbstractValidator<ExpenseClaimRequest>
{
    public ExpenseClaimValidator()
    {
        RuleFor(c => c.ReceiptNumber).NotEmpty()
            .WithMessage(ExpenseClaimMessages.ReceiptNumberIsRequired);
    }
}