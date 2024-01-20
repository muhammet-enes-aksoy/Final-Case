using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validator;
public class ContactValidator : AbstractValidator<ContactRequest>
{
    public ContactValidator()
    {
        RuleFor(c => c.ContactType).NotEmpty()
            .WithMessage(ContactMessages.ContactTypeIsRequired)
            .MaximumLength(20)
            .WithMessage(ContactMessages.ContactTypeMaxLength);

        RuleFor(c => c.Information).NotEmpty()
            .WithMessage(ContactMessages.InformationIsRequired)
            .MinimumLength(10)
            .WithMessage(ContactMessages.InformationMinLength)
            .MaximumLength(50)
            .WithMessage(ContactMessages.InformationMaxLength);

        RuleFor(c => c.UserId).NotEmpty()
            .WithMessage(ContactMessages.UserIdIsRequired);
    }
}