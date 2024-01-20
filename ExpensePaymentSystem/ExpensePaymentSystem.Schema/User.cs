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
    public int Status { get; set; }

    public virtual List<Contact> Contacts { get; set; }
    public virtual List<Account> Accounts { get; set; }
    public virtual List<Address> Addresses { get; set; }
    public virtual List<ExpenseClaim> ExpenseClaims { get; set; }
}