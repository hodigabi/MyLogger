using Microsoft.Extensions.Logging;
using MyLogger.Loggers;

namespace MyLogger.LoggerProviders
{
    public sealed class ConsoleLoggerProvider : BaseLoggerProvider, ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new ConsoleLogger(LoggerEncoding));
    }
}
