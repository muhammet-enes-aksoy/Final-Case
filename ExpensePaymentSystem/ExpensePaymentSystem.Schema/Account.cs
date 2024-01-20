using System.Text.Json.Serialization;
using ExpensePaymentSystem.Base.Schema;


namespace ExpensePaymentSystem.Schema;

public class AccountRequest : BaseRequest
{
    [JsonIgnore]
    public int AccountNumber { get; set; }
    public int UserId { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
}


public class AccountResponse : BaseResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
    public DateTime OpenDate { get; set; }

}