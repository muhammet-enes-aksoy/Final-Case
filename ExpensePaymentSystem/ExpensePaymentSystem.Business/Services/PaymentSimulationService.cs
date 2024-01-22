using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using Serilog;
namespace ExpensePaymentSystem.Business.Services;
public class PaymentSimulationService
{
    private readonly HttpClient httpClient;
    private readonly ExpensePaymentSystemDbContext dbContext;
    public PaymentSimulationService(HttpClient httpClient, ExpensePaymentSystemDbContext dbContext)
    {
        this.httpClient = httpClient;
        this.dbContext = dbContext;
    }

    public void SimulatePayments()
    {
        var approvedClaims = dbContext.ExpenseClaims
            .Where(c => c.IsProcessed == false && c.Status == "2")
            .ToList();

        foreach (var claim in approvedClaims)
        {
            // Simülasyon: Ödeme işlemini gerçekleştir (API'ye istek at)
            var success = SimulatePayment(claim);

            if (success)
            {
                // Talep işlendi olarak işaretle
                claim.IsProcessed = true;
                var approvedEmployees = dbContext.Employees
                                            .Where(e => e.Id == claim.EmployeeId).ToList();
                /*foreach(var employee in approvedEmployees){
                        employee.Accounts
                }       */                     
                dbContext.SaveChanges();
                Log.Information($"Payment payment successful. Request ID: {claim.Id}");
            }
            else
            {
                // Hata durumunu logla veya başka bir işlem gerçekleştir
                Log.Error($"Payment payment failed. Request ID:: {claim.Id}");
                // Alternatif olarak başka bir işlem gerçekleştirebilirsiniz.
            }

        }
    }

    private bool SimulatePayment(ExpenseClaim claim)
    {

        try
        {
            var response = httpClient.PostAsync("https://random-data-api.com/api/v2/users?size=2&is_xml=true", null).Result;
            if (response.IsSuccessStatusCode)
            {
                // Yanıt içeriğini oku
                var content = response.Content.ReadAsStringAsync().Result;

                // İstenilen bir JSON özelliğini kontrol et
                if (content.Contains("\"success\": true"))
                {
                    // Başarılı bir şekilde işlem gerçekleşti
                    return true;
                }
            }

            // Başarılı bir yanıt durumunda işlem gerçekleşti olarak kabul et
            return true;
        }
        catch (Exception ex)
        {
            // Hata durumunu logla
            return false;
        }
    }
}
