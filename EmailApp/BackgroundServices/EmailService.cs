using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EmailApp.BackgroundServices;

public class EmailService : BackgroundService
{
    private readonly IConfiguration _config;
    private readonly IConnectionFactory _factory;
    private readonly string _EmailQueue;
    public EmailService(IConfiguration config)
    {
        _config = config;
        _factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQ:HostName"],
            UserName = _config["RabbitMQ:UserName"],
            Password = _config["RabbitMQ:Password"]
        };

        _EmailQueue = _config["RabbitMQ:EmailQueue"];
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _EmailQueue, durable: true, exclusive: false, autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"[x] Received: {message}");

                await SendEmail(message, stoppingToken);
            };

            await channel.BasicConsumeAsync(queue: _EmailQueue, autoAck: true, consumer: consumer);

            // 6. Keep the service running until the app shuts down
            await Task.CompletedTask;
            
            await Task.Delay(200);
        }
    }

    private async Task SendEmail(string message, CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        System.Console.WriteLine($"Email Sent: with message: {message}");
        await Task.CompletedTask;
    }
}