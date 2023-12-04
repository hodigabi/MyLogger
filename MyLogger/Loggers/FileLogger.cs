using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace MyLogger.Loggers
{
    internal sealed class FileLogger : BaseLogger
    {
        private const int MaxFileSizeInBytes = 5 * 1024;
        private readonly string _filePath;
        private int _writtenSize = 0;
        private int _fileCounter = 1;

        private static TextWriter _writer { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="path">Absolute path to the file</param>
        /// <exception cref="ArgumentNullException"></exception>
        public FileLogger(Encoding encoding, string filePath)
            : base(encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding), "Encoding must be specified");

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "File path must be specified");

            _filePath = filePath;

            if (_writer == null)
            {
                if (File.Exists(_filePath))
                {
                    RotateFile();
                }

                if (!Directory.Exists(Path.GetDirectoryName(_filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(_filePath));

                ConstructWriter();
            }
        }

        internal void ConstructWriter()
            => _writer = new StreamWriter(_filePath, true, Encoding) { AutoFlush = true };

        internal override void Write(string message, LogLevel logLevel)
        {
            _writer.WriteLine(message);

            _writtenSize += Encoding.GetByteCount(message);

            if (_writtenSize >= MaxFileSizeInBytes)
            {
                this.ArchiveFile();
                _writtenSize = 0;
            }
        }

        private void ArchiveFile()
        {
            _writer.Flush();
            try
            {
                _writer.Close();
                _writer.Dispose();
            }
            catch { }

            RotateFile();

            ConstructWriter();
        }

        private void RotateFile()
        {
            var archiveFilePath = ConstructArchivePath();
            File.Move(_filePath, archiveFilePath);
        }

        /// <summary>
        /// File format must be #{LogFileName}.#NextNumber.#{LogFileExtension}
        /// </summary>
        internal string ConstructArchivePath()
        {
            var logFileName = Path.GetFileNameWithoutExtension(_filePath);
            var extension = Path.GetExtension(_filePath);

            var newFileName = $"{logFileName}.{_fileCounter}{extension}";
            var newFilePath = Path.Combine(Path.GetDirectoryName(_filePath), newFileName);
            while (File.Exists(newFilePath))
            {
                _fileCounter++;
                newFilePath = ConstructArchivePath();
            }

            return newFilePath;
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
