using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("MyLogger.Tests")]
namespace MyLogger.Loggers
{
    internal sealed class LoggerConfiguration
    {
        public static readonly HashSet<LogLevel> EnabledLogLeves = new HashSet<LogLevel>()
        {
            LogLevel.Debug,
            LogLevel.Information,
            LogLevel.Error,
        };
    }

    internal abstract class BaseLogger : ILogger, IDisposable
    {
        internal Encoding Encoding { get; private set; }

        public BaseLogger(Encoding encoding)
        {
            Encoding = encoding;
        }

        internal virtual string Format(LogLevel level, string message)
        {
            message ??= string.Empty;

            // #{LogTime} [#{LogLevel}] #{LogMessage}
            return ($"{DateTime.UtcNow} [{level}] {message}");
        }

        public bool IsEnabled(LogLevel logLevel) =>
            LoggerConfiguration.EnabledLogLeves.Contains(logLevel);

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public virtual void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            Write(this.Format(logLevel, formatter(state, null)), logLevel);
        }

        internal abstract void Write(string message, LogLevel logLevel);
        public abstract void Dispose();
    }
}