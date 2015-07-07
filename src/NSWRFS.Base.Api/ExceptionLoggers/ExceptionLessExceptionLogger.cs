using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NSWRFS.Base.Api.ExceptionLoggers
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.ExceptionHandling;

    public class ExceptionLessExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            base.Log(context);
        }

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            return base.LogAsync(context, cancellationToken);
        }

        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return base.ShouldLog(context);
        }
    }
}