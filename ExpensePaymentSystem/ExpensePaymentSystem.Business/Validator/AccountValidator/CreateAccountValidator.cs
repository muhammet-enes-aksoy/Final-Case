using FluentValidation;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.Business.Validator
{
    // Validator class for AccountRequest
    public class CreateAccountValidator : AbstractValidator<AccountRequest>
    {
        public CreateAccountValidator()
        {
            // EmployeeId should be greater than 0
            RuleFor(x => x.EmployeeId).GreaterThan(0).WithMessage("EmployeeId must be greater than 0");

            // IBAN is required and should have a maximum length of 34 characters
            RuleFor(x => x.IBAN).NotEmpty().MaximumLength(34).WithMessage("IBAN is required and should have a maximum length of 34 characters");

            // Balance should be greater than or equal to 0
            RuleFor(x => x.Balance).GreaterThanOrEqualTo(0).WithMessage("Balance should be greater than or equal to 0");

            // CurrencyType is required and should have a maximum length of 3 characters
            RuleFor(x => x.CurrencyType).NotEmpty().MaximumLength(3).WithMessage("CurrencyType is required and should have a maximum length of 3 characters");

            // Name should have a maximum length of 100 characters
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Name should have a maximum length of 100 characters");
        }
    }
}
