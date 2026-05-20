using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SMSApp.BackgroundServices;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddHostedService<SmsService>();

using IHost host = builder.Build();
await host.RunAsync();