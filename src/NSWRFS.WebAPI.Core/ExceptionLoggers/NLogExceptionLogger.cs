// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NLogExceptionLogger.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the NLogExceptionLogger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.WebAPI.Core.ExceptionLoggers
{
    using System.Net.Http;
    using System.Text;
    using System.Web.Http.ExceptionHandling;

    using NLog;

    /// <summary>
    /// The NLog exception logger.
    /// </summary>
    public class NLogExceptionLogger : ExceptionLogger
    {
        /// <summary>
        /// The NLog logger.
        /// </summary>
        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void Log(ExceptionLoggerContext context)
        {
            Nlog.Log(LogLevel.Error, context.Exception, RequestToString(context.Request));
        }

        /// <summary>
        /// The request to string.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string RequestToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();
            if (request.Method != null)
            {
                message.Append(request.Method);
            }

            if (request.RequestUri != null)
            {
                message.Append(" ").Append(request.RequestUri);
            }

            return message.ToString();
        }
    }
}