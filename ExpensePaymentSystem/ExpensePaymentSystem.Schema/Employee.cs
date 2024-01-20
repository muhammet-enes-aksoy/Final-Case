using System.Text.Json.Serialization;
using ExpensePaymentSystem.Base.Schema;

namespace ExpensePaymentSystem.Schema;
public class EmployeeRequest : BaseRequest
{
    [JsonIgnore]
    public int EmployeeNumber { get; set; }
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public virtual List<AddressRequest> Addresses { get; set; }
    public virtual List<ContactRequest> Contacts { get; set; }
    public virtual List<AccountRequest> Accounts { get; set; }
    public virtual List<ExpenseClaimRequest> ExpenseClaims { get; set; }
}
public class EmployeeResponse : BaseResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public DateTime LastActivityDate { get; set; }
    public string EmployeeName
    {
        get { return FirstName + " " + LastName; }
    }
    public virtual List<AddressResponse> Addresses { get; set; }
    public virtual List<ContactResponse> Contacts { get; set; }
    public virtual List<AccountResponse> Accounts { get; set; }
    public virtual List<ExpenseClaimResponse> ExpenseClaims { get; set; }
    
    
}