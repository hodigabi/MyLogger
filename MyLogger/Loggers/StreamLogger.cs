using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace MyLogger.Loggers
{
    internal class StreamLogger : BaseLogger
    {
        internal static TextWriter _writer { get; set; }

        public StreamLogger(Encoding encoding, TextWriter textWriter) : base(encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding), "Encoding must be specified");

            if (_writer == null)
            {
                _writer = textWriter
                    ?? throw new ArgumentNullException(nameof(textWriter), "A writer instance must be specified");
            }
        }

        internal override void Write(string message, LogLevel logLevel)
        {
            _writer.WriteLine(message);
        }

        public override void Dispose()
        {
            _writer.Flush();
            _writer.Close();

            try
            {
                _writer.Dispose();
            }
            catch { }
        }
    }
}
