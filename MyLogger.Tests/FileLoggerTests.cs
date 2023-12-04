using Microsoft.Extensions.Logging;
using MyLogger.Loggers;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace MyLogger.Tests
{
    public class FileLoggerTests
    {
        private string filePath1 = "C:\\test1\\testlog.txt";
        private string filePath2 = "C:\\test2\\testlog.txt";

        /// <summary>
        /// The unit tests of this class are testing the basic functionality
        /// however, the file system should have been mocked
        /// </summary>
        public FileLoggerTests()
        {
            CleanFiles(filePath1);
            CleanFiles(filePath2);
        }

        [Fact]
        public void ContructArchivePath_ShouldIndexFileName()
        {
            var fileLogger = new FileLogger(Encoding.UTF8, filePath1);

            //#{LogFileName}.#NextNumber.#{LogFileExtension}
            var expectedName1 = "C:\\test1\\testlog.1.txt";

            // yes, run 2 should result the same result because
            // no actual testlog.1.txt is created
            var expectedName2 = "C:\\test1\\testlog.1.txt";

            // Act
            var archiveName1 = fileLogger.ConstructArchivePath();
            var archiveName2 = fileLogger.ConstructArchivePath();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(expectedName1, archiveName1);
                Assert.Equal(expectedName2, archiveName2);
            });
        }

        [Fact(Skip = "Runner/environment dependent")]
        public void LotOfLogs_RotateFile()
        {
            // Arrange
            var fileLogger = new FileLogger(Encoding.UTF8, filePath2);
            var longLogMessage = string.Concat(Enumerable.Repeat("AA", 5100));

            // Act
            fileLogger.Log(LogLevel.Information, longLogMessage);
            fileLogger.Log(LogLevel.Information, ".");

            // Assert
            Assert.True(File.Exists("C:\\test2\\testlog.1.txt"));
        }

        private void CleanFiles(string path)
        {
            path = Path.GetDirectoryName(path);

            DirectoryInfo directoryInfo = new(path);

            if (directoryInfo.Exists)
            {
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
            }
        }
    }
}
