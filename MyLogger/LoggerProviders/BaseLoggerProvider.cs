using MyLogger.Loggers;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace MyLogger.LoggerProviders
{
    public abstract class BaseLoggerProvider
    {
        internal readonly ConcurrentDictionary<string, BaseLogger> _loggers =
            new ConcurrentDictionary<string, BaseLogger>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets or sets the encoding of the logger. Default value is UTF8
        /// </summary>
        public static Encoding LoggerEncoding { get; set; } = Encoding.UTF8;

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
