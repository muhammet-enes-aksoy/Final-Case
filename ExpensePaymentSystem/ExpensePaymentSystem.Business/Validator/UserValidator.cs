using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Validator;
using ExpensePaymentSystem.Schema;
using FluentValidation;


namespace ExpensePaymentSystem.Business.Validator;

public class UserValidator : AbstractValidator<UserRequest>
{
    public UserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.IdentityNumber).NotEmpty().MaximumLength(11).WithName(UserMessages.IdentityNumberDisplayedName);

    }
}