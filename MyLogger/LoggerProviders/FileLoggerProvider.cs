using Microsoft.Extensions.Logging;
using MyLogger.Loggers;

namespace MyLogger.LoggerProviders
{
    public sealed class FileLoggerProvider : BaseLoggerProvider, ILoggerProvider
    {
        /// <summary>
        /// Gets or sets the absolute path of the logfile
        /// </summary>
        public static string FilePath { get; set; }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new FileLogger(LoggerEncoding, FilePath));
    }
}
