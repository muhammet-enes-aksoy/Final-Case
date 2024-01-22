using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validator;
public class CreateCategoryValidator : AbstractValidator<CategoryRequest>
{
    // Validator class for CategoryRequest
    public CreateCategoryValidator()
    {
        // CategoryType is required and should have a maximum length of 50 characters
        RuleFor(c => c.CategoryType).NotEmpty()
            .WithMessage(CategoryMessages.CategoryTypeIsRequired)
            .MaximumLength(100)
            .WithMessage(CategoryMessages.CategoryTypeMaxLength);
    }
}
