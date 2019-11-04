using Microsoft.Extensions.Logging;
using System;

namespace AspNetCoreApiStarter.Shared.Logger
{
    //public class LoggerHelper<T> : LoggerHelper, ILoggerHelper<T>
    //{
    //    public LoggerHelper(ILogger logger):base(logger)
    //    {

    //    }
    //}

    /// <summary>
    /// Helper class for handling informations, warnings and errors logs
    /// </summary>
    public class LoggerHelper<T> : ILoggerHelper<T>
    {
        /// <summary>
        /// Serilog logger
        /// </summary>
        private ILogger<T> _logger;

        /// <summary>
        /// Creates an instance of <see cref="LoggerHelper" />.
        /// </summary>
        /// <param name="logger">Logger pipeline</param>
        public LoggerHelper(ILogger<T> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Logs traces
        /// </summary>
        /// <param name="trace">Trace to log</param>
        public void LogTrace(string trace) => _logger.LogTrace(trace);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trace"></param>
        public void LogVerbose(string trace) => _logger.LogTrace(trace);
        
        /// <summary>
        /// Logs debugs
        /// </summary>
        /// <param name="debug">Debug to log</param>
        public void LogDebug(string debug) => _logger.LogDebug(debug);

        /// <summary>
        /// Logs informations
        /// </summary>
        /// <param name="info">Information to log</param>
        public void LogInfo(string info) => _logger.LogInformation(info);

        /// <summary>
        /// Logs warnings
        /// </summary>
        /// <param name="warning">Information to log</param>
        public void LogWarning(string warning) => _logger.LogWarning(warning);

        /// <summary>
        /// Logs errors
        /// </summary>
        /// <param name="ex">Current exception</param>
        public void LogError(Exception ex) => _logger.LogError(System.Threading.Thread.CurrentThread.ManagedThreadId, ex, ex.Message);
        public void LogException(Exception ex) => _logger.LogError(System.Threading.Thread.CurrentThread.ManagedThreadId, ex, ex.Message);

        /// <summary>
        /// Logs criticals
        /// </summary>
        /// <param name="ex">Current critical</param>
        public void LogCritical(Exception ex) => _logger.LogCritical(System.Threading.Thread.CurrentThread.ManagedThreadId, ex, ex.Message);
    }
}
