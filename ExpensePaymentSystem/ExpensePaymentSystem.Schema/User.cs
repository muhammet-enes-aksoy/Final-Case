using System.Text.Json.Serialization;
using ExpensePaymentSystem.Base.Schema;
using ExpensePaymentSystem.Data.Entity;

namespace ExpensePaymentSystem.Schema;
public class UserRequest : BaseRequest
{
    [JsonIgnore]
    public int UserNumber { get; set; }
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
}
public class UserResponse : BaseResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public DateTime LastActivityDate { get; set; }
    
}