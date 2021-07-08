using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForOnlyOffice.Logging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string _name;

        public FileLoggerProvider(string name)
        {
            _name = name;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_name);
        }

        public void Dispose()
        {

        }
    }
}
