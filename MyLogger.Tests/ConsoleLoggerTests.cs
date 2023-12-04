using Microsoft.Extensions.Logging;
using MyLogger.Exceptions;
using MyLogger.Loggers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace MyLogger.Tests
{
    public class ConsoleLoggerTests
    {
        private readonly ConsoleLogger _consoleLogger;

        private Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; } = new Dictionary<LogLevel, ConsoleColor>()
        {
            [LogLevel.Error] = ConsoleColor.Red,
            [LogLevel.Information] = ConsoleColor.Green,
            [LogLevel.Debug] = ConsoleColor.Gray,
        };

        public ConsoleLoggerTests()
        {
            _consoleLogger = new ConsoleLogger(Encoding.UTF8);
        }

        [Fact]
        public void LogMessage_ShouldThrow_WhenTooLogMessage()
        {
            // Arrange
            var longString = string.Concat(Enumerable.Repeat("XY", 501));

            // Act & Assert
            Assert.Throws<MessageLengthException>(() => _consoleLogger.Log(LogLevel.Debug, longString));
        }

        [Theory]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Debug)]
        public void LogMessage_ConsoleForegroundIsSet_BasedOnLogLevel(LogLevel logLevel)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            _consoleLogger.Log(logLevel, "Should be colored");

            Assert.Equal(LogLevelToColorMap[logLevel], Console.ForegroundColor);
        }

        [Fact]
        // This tests the base class' functionality
        public void LogMessage_Format()
        {
            // #{LogTime} [#{LogLevel}] #{LogMessage}

            // Arrange
            LogLevel logLevel = LogLevel.Error;
            var logMessage = "This is a log message";

            var expectedLogMessage = $"{DateTime.UtcNow} [{logLevel}] {logMessage}{Environment.NewLine}";

            var consoleWriter = new StringWriter();
            Console.SetOut(consoleWriter);

            // Act
            _consoleLogger.Log(logLevel, logMessage);

            // Assert
            Assert.Equal(expectedLogMessage, consoleWriter.ToString());
        }
    }
}
