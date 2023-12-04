using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyLogger.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();

builder.Logging.AddConsoleLogger();

//builder.Logging.AddFileLogger("c:\\test\\log.txt");

//var writer = new StreamWriter("c:\\test\\home.txt")
//{
//    AutoFlush = true
//};
//builder.Logging.AddStreamLogger(writer);

builder.Logging.SetMinimumLevel(LogLevel.Debug);

using IHost host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

logger.Log(LogLevel.Debug, "This should be GRAY");
logger.Log(LogLevel.Information, "This should be GREEN");
logger.Log(LogLevel.Error, "This should be RED");

host.Run();