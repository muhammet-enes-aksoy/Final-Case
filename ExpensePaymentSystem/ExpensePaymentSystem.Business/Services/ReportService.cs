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


    public ApprovalStatusReportModel GetApprovalStatusReport(DateTime reportDate)
    {
        using (var connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
        {
            connection.Open();

            var parameters = new { ReportDate = reportDate.Date };

            string approvedSql = "SELECT COALESCE(SUM(Amount), 0) FROM ExpenseClaim WHERE Status = 1 AND CAST(ClaimDate AS DATE) = @ReportDate";
            decimal approvedAmount = connection.ExecuteScalar<decimal>(approvedSql, parameters);

            string rejectedSql = "SELECT COALESCE(SUM(Amount), 0) FROM ExpenseClaim WHERE Status = 2 AND CAST(ClaimDate AS DATE) = @ReportDate";
            decimal rejectedAmount = connection.ExecuteScalar<decimal>(rejectedSql, parameters);

            return new ApprovalStatusReportModel
            {
                ReportDate = reportDate,
                ApprovedAmount = approvedAmount,
                RejectedAmount = rejectedAmount,
                // Diğer özellikleri buraya ekleyin
            };
        }
    }
}
