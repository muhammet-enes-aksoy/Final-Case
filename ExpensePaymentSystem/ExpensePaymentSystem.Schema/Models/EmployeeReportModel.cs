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

// ApprovalStatusReportItemModel: Onay durumu raporlarında kullanılan iç içe model
public class ApprovalStatusReportItemModel
{
    public DateTime ReportDate { get; set; }
    public decimal Amount { get; set; }
    // Diğer özellikleri ekleyin
}

// ApprovalStatusReportModel: Onay durumu raporları için model
public class ApprovalStatusReportModel
{
    public DateRange ReportDateRange { get; set; }
    public Dictionary<DateTime, ApprovalStatusReportItemModel> ApprovedAmounts { get; set; }
    public Dictionary<DateTime, ApprovalStatusReportItemModel> RejectedAmounts { get; set; }
    // Diğer özellikleri ekleyin
}

// DateRange: Tarih aralığı temsil eden model
public class DateRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    // Diğer özellikleri ekleyin
}