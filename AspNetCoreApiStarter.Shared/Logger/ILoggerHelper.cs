using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreApiStarter.Shared.Logger
{
    public interface ILoggerHelper<T> : ILoggerHelper
    {
    }

    /// <summary>
    /// Logger Helper Interface
    /// </summary>
    public interface ILoggerHelper
    {
        /// <summary>
        /// Logs traces
        /// </summary>
        /// <param name="trace">Trace to log</param>
        void LogTrace(string trace);
        void LogVerbose(string trace);

        /// <summary>
        /// Logs debugs
        /// </summary>
        /// <param name="debug">Debug to log</param>
        void LogDebug(string debug);

        /// <summary>
        /// Logs informations
        /// </summary>
        /// <param name="info">Information to log</param>
        void LogInfo(string info);

        /// <summary>
        /// Logs warnings
        /// </summary>
        /// <param name="warning">Information to log</param>
        void LogWarning(string warning);

        /// <summary>
        /// Logs errors
        /// </summary>
        /// <param name="ex">Current exception</param>
        void LogError(Exception ex);
        void LogException(Exception ex);
        
        /// <summary>
        /// Logs criticals
        /// </summary>
        /// <param name="ex">Current critical</param>
        void LogCritical(Exception ex);
    }
}
