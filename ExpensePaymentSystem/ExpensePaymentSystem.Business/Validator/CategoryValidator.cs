using ExpensePaymentSystem.Business.Constants;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validator;
public class CategoryValidator : AbstractValidator<CategoryRequest>
{
    public CategoryValidator()
    {
        RuleFor(c => c.CategoryType).NotEmpty()
            .WithMessage(CategoryMessages.CategoryTypeIsRequired)
            .MaximumLength(100)
            .WithMessage(CategoryMessages.CategoryTypeMaxLength);
    }
}