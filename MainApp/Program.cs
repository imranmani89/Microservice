var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


// System.Console.WriteLine(builder.Configuration.GetSection("RabbitMQ:HostName").Value!.ToString());

var app = builder.Build();

app.MapControllers();
await app.RunAsync();