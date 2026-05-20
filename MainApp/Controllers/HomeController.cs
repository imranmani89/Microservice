using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace MainApp.Controllers;

[ApiController]
[Route("/[controller]")]
public class HomeController : ControllerBase
{
    private readonly IConfiguration _config;
    public HomeController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("run")]
    public async Task<IActionResult> ApiCheck([FromQuery] string Name = "Nobody", string Total = "100 Rs")
    {
        var factory = new ConnectionFactory
        {
            HostName = _config["RabbitMQ:HostName"],
            UserName = _config["RabbitMQ:UserName"],
            Password = _config["RabbitMQ:Password"]
        };

        using var connection  = await factory.CreateConnectionAsync();
        using var  channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "EmailQueue", durable: true, exclusive: false, autoDelete: false);

        string stringBody = $"Order Saved for {Name} with {Total}. at {DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss")}";
        var body = Encoding.UTF8.GetBytes(stringBody);
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "EmailQueue", body: body);
        System.Console.WriteLine(stringBody);

        return Ok(stringBody);
    }
}