using FluentValidation;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.Business.Validator
{
    // Validator class for AddressRequest
    public class CreateAddressValidator : AbstractValidator<AddressRequest>
    {
        public CreateAddressValidator()
        {
            // EmployeeId should be greater than 0
            RuleFor(x => x.EmployeeId).GreaterThan(0).WithMessage("EmployeeId must be greater than 0");

            // Address1 is required and should have a maximum length of 150 characters
            RuleFor(x => x.Address1).NotEmpty().MaximumLength(150).WithMessage("Address1 is required and should have a maximum length of 150 characters");

            // Country is required and should have a maximum length of 100 characters
            RuleFor(x => x.Country).NotEmpty().MaximumLength(100).WithMessage("Country is required and should have a maximum length of 100 characters");

            // City is required and should have a maximum length of 100 characters
            RuleFor(x => x.City).NotEmpty().MaximumLength(100).WithMessage("City is required and should have a maximum length of 100 characters");

            // PostalCode should have a maximum length of 10 characters
            RuleFor(x => x.PostalCode).MaximumLength(10).WithMessage("PostalCode should have a maximum length of 10 characters");
        }
    }
}
