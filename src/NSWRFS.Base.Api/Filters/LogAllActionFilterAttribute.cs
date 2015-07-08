using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NSWRFS.Base.Api.Filters
{
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using Microsoft.AspNet.Identity;

    using NLog;

    public class LogAllActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The NLog logger.
        /// </summary>
        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Form the log string
            var requestString = actionContext.Request.ToString();

            var principle = actionContext.RequestContext.Principal;
            var username = principle.Identity.IsAuthenticated ? principle.Identity.GetUserName() : "anonymous";

            // Write a Log
            Nlog.Log(LogLevel.Trace, string.Format("request : {0}, username : {1}", requestString, username));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // Form the log string
            var responseString = actionExecutedContext.Response.ToString();

            var principle = actionExecutedContext.ActionContext.RequestContext.Principal;
            var username = principle.Identity.IsAuthenticated ? principle.Identity.GetUserName() : "anonymous";

            // Write a Log
            Nlog.Log(LogLevel.Trace, string.Format("request : {0}, username : {1}", responseString, username));
        }
    }
}