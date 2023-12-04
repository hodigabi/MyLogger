using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MyLogger.LoggerProviders;
using System.IO;
using System.Text;

namespace MyLogger.Extensions
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Registers the custom console logger with UTF8 encoding
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddConsoleLogger(this ILoggingBuilder builder)
            => AddConsoleLogger(builder, Encoding.UTF8);

        /// <summary>
        /// Registers the custom console logger
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="encoding">Encoding of the console</param>
        /// <returns></returns>
        public static ILoggingBuilder AddConsoleLogger(
            this ILoggingBuilder builder, Encoding encoding)
        {
            ConsoleLoggerProvider.LoggerEncoding = encoding;

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, ConsoleLoggerProvider>());

            return builder;
        }

        /// <summary>
        /// Registers the custom file logger with UTF8 encoding
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="filePath">Absolute path of the log file</param>
        /// <returns></returns>
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, string filePath)
            => AddFileLogger(builder, filePath, Encoding.UTF8);


        /// <summary>
        /// Registers the custom file logger
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="filePath">Absolute path of the log file</param>
        /// <param name="encoding">Encoding of the file</param>
        /// <returns></returns>
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, string filePath, Encoding encoding)
        {
            FileLoggerProvider.LoggerEncoding = encoding;
            FileLoggerProvider.FilePath = filePath;

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());

            return builder;
        }

        /// <summary>
        /// Registeres the custom stream logger
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="textWriter">The stream of the logs</param>
        /// <returns></returns>
        public static ILoggingBuilder AddStreamLogger(this ILoggingBuilder builder, TextWriter textWriter)
            => AddStreamLogger(builder, textWriter, Encoding.UTF8);

        /// <summary>
        /// Registeres the custom stream logger
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="textWriter">The stream of the logs</param>
        /// <param name="encoding">Encoding of the stream</param>
        /// <returns></returns>
        public static ILoggingBuilder AddStreamLogger(this ILoggingBuilder builder, TextWriter textWriter, Encoding encoding)
        {
            StreamLoggerProvider.LoggerEncoding = encoding;
            StreamLoggerProvider.TextWriter = textWriter;

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, StreamLoggerProvider>());

            return builder;
        }
    }
}
