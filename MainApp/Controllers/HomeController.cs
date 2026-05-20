using System.Data;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Shared.Models;
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

        var orderList = new List<Order>
        {
            new Order
            {
                OrderNumber = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                OrderedByName = "Imran",
                OrderByContact = "0123456789",
                OrderByEmail = "imran@imran.com",
                TotalAmount = 3000
            },

            new Order
            {
                OrderNumber = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                OrderedByName = "Hasan",
                OrderByContact = "0123456566",
                OrderByEmail = "hasan@hasan.com",
                TotalAmount = 360
            },

            new Order
            {
                OrderNumber = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                OrderedByName = "Asif",
                OrderByContact = "012345559",
                OrderByEmail = "asif@asif.com",
                TotalAmount = 568
            },
            new Order
            {
                OrderNumber = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                OrderedByName = "Abdullah",
                OrderByContact = "012852789",
                OrderByEmail = "ab@ab.com",
                TotalAmount = 200
            }
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "EmailQueue", durable: true, exclusive: false, autoDelete: false);

        orderList.ForEach(async (o) =>
        {
            var stringBody = JsonSerializer.Serialize(o);
            var body = Encoding.UTF8.GetBytes(stringBody);
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "EmailQueue", body: body);
            System.Console.WriteLine(stringBody);
        });


        return Ok("Orders Created");
    }
}