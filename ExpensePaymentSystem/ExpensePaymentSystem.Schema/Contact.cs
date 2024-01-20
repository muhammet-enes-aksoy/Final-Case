using System.Text.Json.Serialization;
using ExpensePaymentSystem.Base.Schema;

namespace ExpensePaymentSystem.Schema;
public class ContactRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public int EmployeeId { get; set; }
    public string ContactType { get; set; }
    public string Information { get; set; }
    public bool IsDefault { get; set; }
}


public class ContactResponse : BaseResponse
{
    public string EmployeeName { get; set; }
    public string ContactType { get; set; }
    public string Information { get; set; }
}