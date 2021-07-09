using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForOnlyOffice.Logging
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFileLogger(this ILoggerFactory factory)
        {
            factory.AddProvider(new FileLoggerProvider());
            return factory;
        }
    }
}
