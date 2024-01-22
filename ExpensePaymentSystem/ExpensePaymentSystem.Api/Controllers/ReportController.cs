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

    [HttpGet("{employeeId}/ExpensesReport")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetEmployeeExpenseReport(int employeeId)
    {
        var report = _reportService.GetEmployeeReport(employeeId);
        return Ok(report);
    }

    [HttpGet("PaymentIntensity")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetPaymentIntensityReport(DateTime startDate, DateTime endDate)
    {
        var report = _reportService.GetPaymentIntensityReport(startDate, endDate);
        return Ok(report);
    }

    [HttpGet("{employeeId}/PaymentIntensity")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetEmployeePaymentIntensityReport(int employeeId, DateTime startDate, DateTime endDate)
    {
        var report = _reportService.GetEmployeePaymentIntensityReport(employeeId, startDate, endDate);
        return Ok(report);
    }

    [HttpGet("ApprovalStatus")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetApprovalStatusReport(DateTime startDate, DateTime endDate)
    {
        var report = _reportService.GetApprovalStatusReport(startDate, endDate);
        return Ok(report);
    }
}
