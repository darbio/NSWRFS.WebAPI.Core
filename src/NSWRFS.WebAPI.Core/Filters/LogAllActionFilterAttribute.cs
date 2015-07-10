// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogAllActionFilterAttribute.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the LogAllActionFilterAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.WebAPI.Core.Filters
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using Microsoft.AspNet.Identity;

    using NLog;

    /// <summary>
    /// The log all action filter attribute.
    /// </summary>
    public class LogAllActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The NLog logger.
        /// </summary>
        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Form the log string
            var requestString = actionContext.Request.ToString().Replace(Environment.NewLine, " ");

            var principle = actionContext.RequestContext.Principal;
            var username = principle.Identity.IsAuthenticated ? principle.Identity.GetUserName() : "anonymous";

            // Write a Log
            var logObject = new { request = requestString, identity = username };
            Nlog.Log(LogLevel.Trace, logObject);
        }

        /// <summary>
        /// The on action executing async.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return Task.Run(() => this.OnActionExecuting(actionContext), cancellationToken);
        }

        /// <summary>
        /// The on action executed.
        /// </summary>
        /// <param name="actionExecutedContext">
        /// The action executed context.
        /// </param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // Form the log string
            var responseString = actionExecutedContext.Response.ToString().Replace(Environment.NewLine, " ");

            var principle = actionExecutedContext.ActionContext.RequestContext.Principal;
            var username = principle.Identity.IsAuthenticated ? principle.Identity.GetUserName() : "anonymous";

            // Write a Log
            var logObject = new { response = responseString, identity = username };
            Nlog.Log(LogLevel.Trace, logObject);
        }

        /// <summary>
        /// The on action executed async.
        /// </summary>
        /// <param name="actionExecutedContext">
        /// The action executed context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.Run(() => this.OnActionExecuted(actionExecutedContext), cancellationToken);
        }
    }
}