using System.Text.Json;

namespace ExpensePaymentSystem.Base.Response;
// Represents a generic API response without a specific data type.
public class ApiResponse
{
    // Default constructor initializes properties and sets the server date and reference number.
    public ApiResponse(string message = null)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            Success = true;
        }
        else
        {
            Success = false;
            Message = message;
        }
    }

    // Overrides the ToString method to serialize the object to JSON.
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    // Properties of the ApiResponse class.
    public bool Success { get; set; }
    public string Message { get; set; }
    public DateTime ServerDate { get; set; } = DateTime.UtcNow;
    public Guid ReferenceNo { get; set; } = Guid.NewGuid();
}



public class ApiResponse<T>
{
    // Constructors with different parameters to create instances of ApiResponse<T>.
    public ApiResponse(bool isSuccess)
    {
        Success = isSuccess;
        Response = default;
        Message = isSuccess ? "Success" : "Error";
    }

    public ApiResponse(T data)
    {
        Success = true;
        Response = data;
        Message = "Success";
    }

    public ApiResponse(string message)
    {
        Success = false;
        Response = default;
        Message = message;
    }

    // Overrides the ToString method to serialize the object to JSON.
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    // Properties of the ApiResponse<T> class.
    public DateTime ServerDate { get; set; } = DateTime.UtcNow;
    public Guid ReferenceNo { get; set; } = Guid.NewGuid();
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Response { get; set; }
}