using Microsoft.Extensions.Hosting;

namespace SMSApp.BackgroundServices;

public class SmsService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {

        Console.WriteLine("This Sms has been sent");

        return Task.CompletedTask;
    }
}