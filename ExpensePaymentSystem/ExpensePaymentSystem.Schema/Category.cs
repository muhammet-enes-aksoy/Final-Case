using System.Text.Json.Serialization;
using ExpensePaymentSystem.Base.Schema;

namespace ExpensePaymentSystem.Schema;
public class CategoryRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string CategoryType { get; set; }
}
public class CategoryResponse : BaseResponse
{
    public string CategoryType { get; set; }

}