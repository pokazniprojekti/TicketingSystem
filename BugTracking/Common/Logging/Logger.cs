using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public class Logger:ILogger
    {
        private static readonly string MethodStartTemplate = "{0}.{1}() - START";
        private static readonly string MethodTemplate = "{0}.{1}()";
        private static readonly string MethodEndTemplate = "{0}.{1}() - END";
        private static readonly string ConfigFileName = "Logging.config";

        public static bool IsInitialized { get; set; }
        public static void Initialize(string configurationFilePath = null)
        {
            var configFileInfo = configurationFilePath == null
                ? new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName))
                : new FileInfo(configurationFilePath);

            if (!configFileInfo.Exists)
                throw new ApplicationException("Logging configuration file does not exist: " + configFileInfo.FullName);

            XmlConfigurator.ConfigureAndWatch(configFileInfo);

            IsInitialized = true;
        }

        private readonly ILog _tracer;
        private readonly ILog _logger;

        public Logger(string loggerNameForTracing, string loggerNameForLogging)
        {
            _tracer = LogManager.GetLogger(loggerNameForTracing);
            _logger = LogManager.GetLogger(loggerNameForLogging);

        }

        //TRACING
        public void TraceDebug(string message)
        {
            _tracer.Debug(message);
        }
        public void TraceDebugFormat(string message, params object[] args)
        {
            _tracer.DebugFormat(message, args);
        }
        public void TraceInfo(string message)
        {
            _tracer.Info(message);
        }
        public void TraceInfoFormat(string message, params object[] args)
        {
            _tracer.InfoFormat(message, args);
        }
        public void TraceWarning(string message)
        {
            _tracer.Warn(message);
        }
        public void TraceWarningFormat(string message, params object[] args)
        {
            _tracer.WarnFormat(message, args);
        }
        public void TraceError(string message, Exception ex = null)
        {
            _tracer.Error(message, ex);
        }
        public void TraceErrorFormat(string message, params object[] args)
        {
            _tracer.ErrorFormat(message, args);
        }
        public void TraceFatal(string message, Exception ex = null)
        {
            _tracer.Fatal(message, ex);
        }
        public void TraceFatalFormat(string message, params object[] args)
        {
            _tracer.FatalFormat(message, args);
        }
        public void TraceMethod(string className, [CallerMemberName] string methodName = "")
        {
            _tracer.DebugFormat(MethodTemplate, className, methodName);
        }
        public void TraceMethodWithMessage(string message, string className, [CallerMemberName] string methodName = "")
        {
            _tracer.Debug($"{string.Format(MethodTemplate, className, methodName)}. {message}");
        }
        public void TraceMethodStart(string className, [CallerMemberName] string methodName = "")
        {
            _tracer.DebugFormat(MethodStartTemplate, className, methodName);
        }
        public void TraceMethodStartWithMessage(string message, string className, [CallerMemberName] string methodName = "")
        {
            _tracer.DebugFormat("{0}. {1}",
                string.Format(MethodStartTemplate, className, methodName),
                message);
        }
        public void TraceMethodEnd(string className, [CallerMemberName] string methodName = "")
        {
            _tracer.DebugFormat(MethodEndTemplate, className, methodName);
        }
        public void TraceMethodEndWithMessage(string message, string className, [CallerMemberName] string methodName = "")
        {
            _tracer.DebugFormat("{0}. {1}",
                string.Format(MethodEndTemplate, className, methodName),
                message);
        }
        // LOGGING
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }
        public void LogDebugFormat(string message, params object[] args)
        {
            _logger.DebugFormat(message, args);
        }
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
        public void LogInfoFormat(string message, params object[] args)
        {
            _logger.InfoFormat(message, args);
        }
        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }
        public void LogWarningFormat(string message, params object[] args)
        {
            _logger.WarnFormat(message, args);
        }
        public void LogError(string message, Exception ex = null)
        {
            _logger.Error(message, ex);
        }
        public void LogErrorFormat(string message, params object[] args)
        {
            _logger.ErrorFormat(message, args);
        }
        public void LogFatal(string message, Exception ex = null)
        {
            _logger.Fatal(message, ex);
        }
        public void LogFatalFormat(string message, params object[] args)
        {
            _logger.FatalFormat(message, args);
        }

        // LOGGING
        public void ServiceDebug(string logger, string message)
        {
            log4net.LogicalThreadContext.Properties["LoggerColumn"] = logger;
            _logger.Debug(message);
        }
        public void ServiceDebugFormat(string logger, string message, params object[] args)
        {
            log4net.LogicalThreadContext.Properties["LoggerColumn"] = logger;
            _logger.DebugFormat(message, args);
        }
        public void ServiceInfo(string logger, string message)
        {
            log4net.LogicalThreadContext.Properties["LoggerColumn"] = logger;
            _logger.Info(message);
        }
        public void ServiceInfoFormat(string logger, string message, params object[] args)
        {
            log4net.LogicalThreadContext.Properties["LoggerColumn"] = logger;
            _logger.InfoFormat(message, args);
        }
        public void ServiceWarning(string logger, string message)
        {
            log4net.LogicalThreadContext.Properties["LoggerColumn"] = logger;
            _logger.Warn(message);
        }
        public void ServiceWarningFormat(string logger, string message, params object[] args)
        {
            log4net.LogicalThreadContext.Properties["LoggerColumn"] = logger;
            _logger.WarnFormat(message, args);
        }
        public void ServiceError(string logger, string message, Exception ex = null)
        {
            log4net.LogicalThreadContext.Properties["LoggerColumn"] = logger;
            _logger.Error(message, ex);
        }
        public void ServiceErrorFormat(string logger, string message, params object[] args)
        {
            log4net.LogicalThreadContext.Properties["LoggerColumn"] = logger;
            _logger.ErrorFormat(message, args);
        }
        public void ServiceFatal(string logger, string message, Exception ex = null)
        {
            log4net.LogicalThreadContext.Properties["LoggerColumn"] = logger;
            _logger.Fatal(message, ex);
        }
        public void ServiceFatalFormat(string logger, string message, params object[] args)
        {
            log4net.LogicalThreadContext.Properties["LoggerColumn"] = logger;
            _logger.FatalFormat(message, args);
        }
    }
}
