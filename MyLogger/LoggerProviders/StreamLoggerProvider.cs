using Microsoft.Extensions.Logging;
using MyLogger.Loggers;
using System.IO;

namespace MyLogger.LoggerProviders
{
    public sealed class StreamLoggerProvider : BaseLoggerProvider, ILoggerProvider
    {
        /// <summary>
        /// Gets or sets the log destination
        /// </summary>
        public static TextWriter TextWriter { get; set; }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new StreamLogger(LoggerEncoding, TextWriter));
    }
}