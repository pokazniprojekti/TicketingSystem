using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public class LoggerFactory
    {
        private static readonly string SuffixTrace = "Trace";
        private static readonly string SuffixLog = "Log";

        static LoggerFactory()
        {
            if (!Logger.IsInitialized)
                Logger.Initialize();
        }

        /// <summary>
        /// Returns configured ILogger instance based on the given LoggingComponent value.
        /// </summary>
        /// <param name="loggingComponent">Component that is going to use logging.</param>
        /// <returns>Ready-to-use ILoggger instance</returns>
        public static ILogger GetLogger(LoggingComponent loggingComponent)
        {
            var loggerNameForTracing = $"{loggingComponent}{SuffixTrace}";
            var loggerNameForLogging = $"{loggingComponent}{SuffixLog}";

            return new Logger(loggerNameForTracing, loggerNameForLogging);
        }

        public static ILogger GetLogger(string loggingComponent)
        {
            var loggerNameForTracing = $"{loggingComponent}{SuffixTrace}";
            var loggerNameForLogging = $"{loggingComponent}{SuffixLog}";

            return new Logger(loggerNameForTracing, loggerNameForLogging);
        }
    }
}
