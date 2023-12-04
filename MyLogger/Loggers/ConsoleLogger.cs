using Microsoft.Extensions.Logging;
using MyLogger.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLogger.Loggers
{
    internal sealed class ConsoleLogger : StreamLogger
    {
        private const int MaxMessageLength = 1000;

        private readonly object __lockObj = new object();

        private Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; } = new Dictionary<LogLevel, ConsoleColor>()
        {
            [LogLevel.Error] = ConsoleColor.Red,
            [LogLevel.Information] = ConsoleColor.Green,
            [LogLevel.Debug] = ConsoleColor.Gray,
        };

        public ConsoleLogger(Encoding encoding)
            : base(encoding, Console.Out)
        {
            Console.OutputEncoding = encoding;
        }

        internal override void Write(string message, LogLevel logLevel)
        {
            lock (__lockObj)
            {
                Console.ForegroundColor = LogLevelToColorMap[logLevel];
                Console.WriteLine(ThrowWhenTooLong(message));
            }
        }

        private string ThrowWhenTooLong(string message)
        {
            if (message?.Length > MaxMessageLength)
            {
                throw new MessageLengthException($"'{message}' is longer than the allowed {MaxMessageLength} characters");
            }

            return message;
        }

        public override void Dispose() { }
    }
}
