namespace ExpensePaymentSystem.Business.Constants;
public class AccountMessages
{
    public const string CustomerNotExists = "Customer does not exist with the given customerId";
    public const string RecordNotExists = "Record not found";

    public const string BalanceGreaterThanZero = "Balance must be greater than 0.";
    public const string IbanNotEmpty = "IBAN must not be empty.";
    public const string IbanLength = "IBAN must be 26 characters long.";
    public const string NameNotEmpty = "Name must not be empty.";
    public const string NameLength = "Name must be less than 50 characters long.";
}