using ExpensePaymentSystem.Business.Services;
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

    [HttpGet("employee-report")]
    public IActionResult GetEmployeeReport(int employeeId)
    {
        var report = _reportService.GetEmployeeReport(employeeId);
        return Ok(report);
    }

    [HttpGet("payment-intensity-report")]
    public IActionResult GetPaymentIntensityReport(DateTime startDate, DateTime endDate)
    {
        var report = _reportService.GetPaymentIntensityReport(startDate, endDate);
        return Ok(report);
    }
    
    [HttpGet("approval-status")]
    public IActionResult GetApprovalStatusReport(DateTime reportDate)
    {
        var report = _reportService.GetApprovalStatusReport(reportDate);
        return Ok(report);
    }
}
