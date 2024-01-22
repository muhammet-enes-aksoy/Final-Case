using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Business.Validator;
using ExpensePaymentSystem.Schema;
using FluentValidation;


namespace ExpensePaymentSystem.Business.Validator;

public class CreateEmployeeValidator : AbstractValidator<EmployeeRequest>
{
    public CreateEmployeeValidator()
    {

        RuleFor(e => e.IdentityNumber)
            .NotEmpty()
            .Length(11)
            .WithMessage("IdentityNumber is required and must be 11 characters.");

        RuleFor(e => e.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("FirstName is required and cannot exceed 50 characters.");

        RuleFor(e => e.LastName)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("LastName is required and cannot exceed 50 characters.");

        RuleFor(e => e.Role)
            .NotEmpty()
            .MaximumLength(30)
            .WithMessage("Role is required and cannot exceed 30 characters.");

        RuleFor(e => e.UserName)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("UserName is required and cannot exceed 50 characters.");

        RuleFor(e => e.Password)
            .NotEmpty()
            .MaximumLength(250)
            .WithMessage("Password is required and cannot exceed 250 characters.");

    }
}