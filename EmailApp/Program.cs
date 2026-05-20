using EmailApp.BackgroundServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddHostedService<EmailService>();


using IHost host = builder.Build();
await host.RunAsync();