using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.Business.Constants;
public static class CategoryMessages
{
    public const string CategoryAlreadyExists = "Category already exists.";
    public const string RecordNotExists = "Record not found";
    public const string CategoryTypeIsRequired = "Category type is required";
    public const string CategoryTypeMaxLength = "Category type must not exceed 100 characters";

}