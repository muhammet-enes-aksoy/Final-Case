using System.Text.Json.Serialization;
using ExpensePaymentSystem.Base.Schema;
using ExpensePaymentSystem.Data.Entity;

namespace ExpensePaymentSystem.Schema;
public class ExpenseClaimRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public int PaymentMethodId { get; set; }
    public string PaymentLocation { get; set; }
    public string ReceiptNumber { get; set; }
    public string Status { get; set; }
    public string StatusDescription { get; set; }
    public bool IsProcessed { get; set; }
    public double Amount { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime ConfirmDate { get; set; }
    public bool IsDefault { get; set; }
}
public class ExpenseClaimResponse : BaseResponse
{
    public string UserName { get; set; }
    public string Category { get; set; }
    public string PaymentMethod { get; set; }
    public double Amount { get; set; }
    public string PaymentLocation { get; set; }
    public string ReceiptNumber { get; set; }
    public string Status { get; set; }
    public string StatusDescription { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime ConfirmDate { get; set; }
    public bool IsProcessed { get; set; }

}