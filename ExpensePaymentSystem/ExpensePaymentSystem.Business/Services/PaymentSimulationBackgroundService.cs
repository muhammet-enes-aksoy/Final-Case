using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExpensePaymentSystem.Business.Services;
public class PaymentSimulationBackgroundService : BackgroundService
{
    private readonly PaymentSimulationService _paymentSimulationService;
    private readonly IServiceProvider _serviceProvider;

    public PaymentSimulationBackgroundService(PaymentSimulationService paymentSimulationService, IServiceProvider serviceProvider)
    {
        _paymentSimulationService = paymentSimulationService;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var paymentSimulationService = scope.ServiceProvider.GetRequiredService<PaymentSimulationService>();
                paymentSimulationService.SimulatePayments();
            }
            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }
}
