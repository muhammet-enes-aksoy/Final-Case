using System.Text.Json.Serialization;
using ExpensePaymentSystem.Base.Schema;

namespace ExpensePaymentSystem.Schema;
public class PaymentMethodRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string PaymentMethodType { get; set; }
}
public class PaymentMethodResponse : BaseResponse
{
    public string PaymentMethodType { get; set; }

}