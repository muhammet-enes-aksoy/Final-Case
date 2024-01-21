using System;
using System.Collections.Generic;
using System.Data.SqlClient; // Dapper'ı kullanabilmek için gerekli
using System.Linq;
using Dapper;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Services;
public class ReportService
{
    private readonly ExpensePaymentSystemDbContext _dbContext;

    public ReportService(ExpensePaymentSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<EmployeeReportModel> GetEmployeeReport(int employeeId)
    {
        using (var connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
        {
            connection.Open();

            string sql = "SELECT Id as EmployeeId, FirstName, LastName FROM Employee WHERE Id = @EmployeeId";
            return connection.Query<EmployeeReportModel>(sql, new { EmployeeId = employeeId }).ToList();
        }
    }

    public List<PaymentIntensityReportModel> GetPaymentIntensityReport(DateTime startDate, DateTime endDate)
    {
        using (var connection = new SqlConnection(_dbContext.Database.GetConnectionString()))
        {
            connection.Open();

            string sql = @"SELECT EmployeeId, SUM(Amount) as TotalPaymentAmount, CONVERT(date, ClaimDate) as ReportDate 
                           FROM ExpenseClaim 
                           WHERE ClaimDate BETWEEN @StartDate AND @EndDate
                           GROUP BY EmployeeId, CONVERT(date, ClaimDate)";

            return connection.Query<PaymentIntensityReportModel>(sql, new { StartDate = startDate, EndDate = endDate }).ToList();
        }
    }


    // Şirket için personel bazlı günlük, haftalık ve aylık harcama yoğunluğu raporları
    public List<PaymentIntensityReportModel> GetEmployeePaymentIntensityReport(int employeeId, DateTime startDate, DateTime endDate)
    {
         using (var connection = new SqlConnection(_dbContext.Database.GetConnectionString()))
        {
            connection.Open();

            string sql = @"SELECT CONVERT(date, ClaimDate) as ReportDate, 
                                  SUM(Amount) as TotalPaymentAmount 
                           FROM ExpenseClaim 
                           WHERE EmployeeId = @EmployeeId AND ClaimDate BETWEEN @StartDate AND @EndDate
                           GROUP BY CONVERT(date, ClaimDate)";

            return connection.Query<PaymentIntensityReportModel>(sql, new { EmployeeId = employeeId, StartDate = startDate, EndDate = endDate }).ToList();
        }
    }

    // Şirket için günlük, haftalık ve aylık onaylanan ve red edilen masraf miktarları raporu
    public ApprovalStatusReportModel GetApprovalStatusReport(DateTime startDate, DateTime endDate)
    {
         using (var connection = new SqlConnection(_dbContext.Database.GetConnectionString()))
        {
            connection.Open();

            var parameters = new
            {
                StartDate = startDate.Date,
                EndDate = endDate.Date
            };

            string approvedSql = "SELECT CONVERT(date, ClaimDate) as ReportDate, COALESCE(SUM(Amount), 0) as ApprovedAmount FROM ExpenseClaim WHERE Status = 2 AND CAST(ClaimDate AS DATE) BETWEEN @StartDate AND @EndDate GROUP BY CONVERT(date, ClaimDate)";
            string rejectedSql = "SELECT CONVERT(date, ClaimDate) as ReportDate, COALESCE(SUM(Amount), 0) as RejectedAmount FROM ExpenseClaim WHERE Status = 3 AND CAST(ClaimDate AS DATE) BETWEEN @StartDate AND @EndDate GROUP BY CONVERT(date, ClaimDate)";

            var approvedAmounts = connection.Query<ApprovalStatusReportItemModel>(approvedSql, parameters).ToDictionary(x => x.ReportDate);
            var rejectedAmounts = connection.Query<ApprovalStatusReportItemModel>(rejectedSql, parameters).ToDictionary(x => x.ReportDate);

            var result = new ApprovalStatusReportModel
            {
                ReportDateRange = new DateRange { StartDate = startDate.Date, EndDate = endDate.Date },
                ApprovedAmounts = approvedAmounts,
                RejectedAmounts = rejectedAmounts
                // Diğer özellikleri buraya ekleyin
            };

            return result;
        }
    }
}
