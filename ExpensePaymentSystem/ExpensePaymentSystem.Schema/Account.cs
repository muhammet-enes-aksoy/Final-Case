using System.Text.Json.Serialization;
using ExpensePaymentSystem.Base.Schema;


namespace ExpensePaymentSystem.Schema;

public class AccountRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonIgnore]
    public int AccountNumber { get; set; }
    public int EmployeeId { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
}


public class AccountResponse : BaseResponse
{
    public string EmployeeName { get; set; }
    public int AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
    public DateTime OpenDate { get; set; }

}