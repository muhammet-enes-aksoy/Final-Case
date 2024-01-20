namespace ExpensePaymentSystem.Business.Constants;
public static class ExpenseClaimMessages
{
    public const string DefaultExpenseClaimAlreadyExistsForUserId = "A default Expense claim already exists for the user id: {0}";
    public const string RecordNotExists = "Record not found";
    public const string ExpenseClaimTypeIsRequired = "ExpenseClaim type is required";
    public const string ExpenseClaimTypeMaxLength = "ExpenseClaim type must not exceed 20 characters";
    public const string InformationIsRequired = "Information is required";
    public const string InformationMaxLength = "Information must not exceed 50 characters";
    public const string InformationMinLength = "Information must be at least 10 characters";
    public const string UserIdIsRequired = "User id is required";
}