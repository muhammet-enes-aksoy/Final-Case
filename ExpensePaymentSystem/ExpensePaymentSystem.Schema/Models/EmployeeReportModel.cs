namespace ExpensePaymentSystem.Schema;
public class EmployeeReportModel
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    // Diğer gerekli özellikleri ekleyin
}

public class PaymentIntensityReportModel
{
    public int EmployeeId { get; set; }
    public decimal TotalPaymentAmount { get; set; }
    public DateTime ReportDate { get; set; }
    // Diğer gerekli özellikleri ekleyin
}

public class ApprovalStatusReportModel
{
    public DateTime ReportDate { get; set; }
    public decimal ApprovedAmount { get; set; }
    public decimal RejectedAmount { get; set; }
    // Diğer özellikleri ekleyin
}

