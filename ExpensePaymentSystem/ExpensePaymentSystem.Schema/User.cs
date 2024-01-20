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
    public virtual List<AddressRequest> Addresses { get; set; }
    public virtual List<ContactRequest> Contacts { get; set; }
    public virtual List<AccountRequest> Accounts { get; set; }
    public virtual List<ExpenseClaimRequest> ExpenseClaims { get; set; }
}
public class UserResponse : BaseResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public DateTime LastActivityDate { get; set; }
    public string UserName
    {
        get { return FirstName + " " + LastName; }
    }
    public virtual List<AddressResponse> Addresses { get; set; }
    public virtual List<ContactResponse> Contacts { get; set; }
    public virtual List<AccountResponse> Accounts { get; set; }
    public virtual List<ExpenseClaimResponse> ExpenseClaims { get; set; }
    
    
}