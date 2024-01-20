using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Schema;
using FluentValidation;


namespace ExpensePaymentSystem.Business.Validator;

public class TokenValidator : AbstractValidator<TokenRequest>
{
    public TokenValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
      //  RuleFor(x => x.Password).NotEmpty().MaximumLength(11).WithName(EmployeeMessages.IdentityNumberDisplayedName);

    }
}