using ExpensePaymentSystem.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }

    // Get employee expense report by employeeId (accessible only to Admin).
    [HttpGet("{employeeId}/ExpensesReport")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetEmployeeExpenseReport(int employeeId)
    {
        // Call the ReportService to get the employee expense report.
        var report = _reportService.GetEmployeeReport(employeeId);

        // Return the result as Ok response.
        return Ok(report);
    }

    // Get payment intensity report within the specified date range (accessible only to Admin).
    [HttpGet("PaymentIntensity")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetPaymentIntensityReport(DateTime startDate, DateTime endDate)
    {
        // Call the ReportService to get the payment intensity report.
        var report = _reportService.GetPaymentIntensityReport(startDate, endDate);

        // Return the result as Ok response.
        return Ok(report);
    }

    // Get employee payment intensity report by employeeId within the specified date range (accessible only to Admin).
    [HttpGet("{employeeId}/PaymentIntensity")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetEmployeePaymentIntensityReport(int employeeId, DateTime startDate, DateTime endDate)
    {
        // Call the ReportService to get the employee payment intensity report.
        var report = _reportService.GetEmployeePaymentIntensityReport(employeeId, startDate, endDate);

        // Return the result as Ok response.
        return Ok(report);
    }

    // Get approval status report within the specified date range (accessible only to Admin).
    [HttpGet("ApprovalStatus")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetApprovalStatusReport(DateTime startDate, DateTime endDate)
    {
        // Call the ReportService to get the approval status report.
        var report = _reportService.GetApprovalStatusReport(startDate, endDate);

        // Return the result as Ok response.
        return Ok(report);
    }
}
