using FluentValidation;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.Business.Validator
{
    // Validator class for ContactRequest
    public class CreateContactValidator : AbstractValidator<ContactRequest>
    {
        public CreateContactValidator()
        {
            // EmployeeId should be greater than 0
            RuleFor(x => x.EmployeeId).GreaterThan(0).WithMessage("EmployeeId must be greater than 0");

            // ContactType is required and should have a maximum length of 10 characters
            RuleFor(x => x.ContactType).NotEmpty().MaximumLength(10).WithMessage("ContactType is required and should have a maximum length of 10 characters");

            // Information is required and should have a maximum length of 100 characters
            RuleFor(x => x.Information).NotEmpty().MaximumLength(100).WithMessage("Information is required and should have a maximum length of 100 characters");
        }
    }
}
