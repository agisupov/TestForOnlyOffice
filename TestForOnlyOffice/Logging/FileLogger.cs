using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestForOnlyOffice.Logging
{
    public class FileLogger : ILogger
    {
        private string _name;

        public FileLogger(string name)
        {
            _name = name;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var filePath = $"{_name}_{DateTimeOffset.UtcNow:yyyy-MM-dd}.log";
            var record = string.Format("{0} [{1}] {2} {3}", 
                "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "]", 
                logLevel.ToString(), 
                formatter(state, exception),
                exception != null ? exception.StackTrace : "");

            File.AppendAllText(filePath, record + Environment.NewLine);
        }
    }
}
