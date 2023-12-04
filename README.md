# MyLogger

a simple logger library which can be used in other projects to log different messages using the `#{LogTime} [#{LogLevel}] #{LogMessage}` format

there are three different message levels
- debug
- info
- error

there are three types of the loggers
- console logger: logs to the console
- file logger: logs to a file
- stream logger: logs to any stream

### console logger
- throws a `MessageLengthException` exception if the log message is longer than 1000 characters
- sets the color of the text depending on the message level

```
debug - gray
info - green
error - red
```

#### usage
`builder.Logging.AddConsoleLogger();`

### file logger
rotates the files by size. If a logfile reaches the size of 5k it should be archived with the name `#{LogFileName}.#NextNumber.#{LogFileExtension}` and the logging should be continued with the original filename.

E.g.: original log name is: log.txt. The first rotation should create log.1.txt, the second rotation creates the log.2.txt file.

#### usage
builder.Logging.AddFileLogger("absoluteFilePath");

### stream logger
#### usage

`builder.Logging.AddStreamLogger(TextWriter);`