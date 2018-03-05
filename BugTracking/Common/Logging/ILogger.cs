using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public interface ILogger
    {
        // TRACING
        void TraceDebug(string message);
        void TraceDebugFormat(string message, params object[] args);
        void TraceInfo(string message);
        void TraceInfoFormat(string message, params object[] args);
        void TraceWarning(string message);
        void TraceWarningFormat(string message, params object[] args);
        void TraceError(string message, Exception ex = null);
        void TraceErrorFormat(string message, params object[] args);
        void TraceFatal(string message, Exception ex = null);
        void TraceFatalFormat(string message, params object[] args);
        void TraceMethod(string className, [CallerMemberName] string methodName = "");
        void TraceMethodWithMessage(string message, string className, [CallerMemberName] string methodName = "");
        void TraceMethodStart(string className, [CallerMemberName] string methodName = "");
        void TraceMethodStartWithMessage(string message, string className, [CallerMemberName] string methodName = "");
        void TraceMethodEnd(string className, [CallerMemberName] string methodName = "");
        void TraceMethodEndWithMessage(string message, string className, [CallerMemberName] string methodName = "");

        // LOGGING
        void LogDebug(string message);
        void LogDebugFormat(string message, params object[] args);
        void LogInfo(string message);
        void LogInfoFormat(string message, params object[] args);
        void LogWarning(string message);
        void LogWarningFormat(string message, params object[] args);
        void LogError(string message, Exception ex = null);
        void LogErrorFormat(string message, params object[] args);
        void LogFatal(string message, Exception ex = null);
        void LogFatalFormat(string message, params object[] args);

        // SERVICE LOGGING
        void ServiceDebug(string logger, string message);
        void ServiceDebugFormat(string logger, string message, params object[] args);
        void ServiceInfo(string logger, string message);
        void ServiceInfoFormat(string logger, string message, params object[] args);
        void ServiceWarning(string logger, string message);
        void ServiceWarningFormat(string logger, string message, params object[] args);
        void ServiceError(string logger, string message, Exception ex = null);
        void ServiceErrorFormat(string logger, string message, params object[] args);
        void ServiceFatal(string logger, string message, Exception ex = null);
        void ServiceFatalFormat(string logger, string message, params object[] args);
    }
}
