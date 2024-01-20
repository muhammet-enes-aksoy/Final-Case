namespace ExpensePaymentSystem.Business.Constants;
public static class ContactMessages
{
    public const string DefaultContactAlreadyExistsForEmployeeId = "A default contact already exists for the employee id: {0}";
    public const string RecordNotExists = "Record not found";
    public const string ContactTypeIsRequired = "Contact type is required";
    public const string ContactTypeMaxLength = "Contact type must not exceed 20 characters";
    public const string InformationIsRequired = "Information is required";
    public const string InformationMaxLength = "Information must not exceed 50 characters";
    public const string InformationMinLength = "Information must be at least 10 characters";
    public const string EmployeeIdIsRequired = "Employee id is required";
}