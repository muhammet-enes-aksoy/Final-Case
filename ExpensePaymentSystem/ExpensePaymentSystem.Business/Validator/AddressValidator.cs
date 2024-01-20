using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validator;

public class CreateAddressValidator : AbstractValidator<AddressRequest>
{
    public CreateAddressValidator()
    {
        RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(6).MinimumLength(6)
            .WithName(AddressMessages.PostalCodeDisplayedName);
        RuleFor(x => x.Address1).NotEmpty().MinimumLength(20).MaximumLength(100) 
            .WithName(AddressMessages.Address1DisplayedName);
        RuleFor(x => x.Address2).NotEmpty().MaximumLength(100)
            .WithName(AddressMessages.Address2DisplayedName);
    }
}