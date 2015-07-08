using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NSWRFS.Base.Api.Startup))]

namespace NSWRFS.Base.Api
{
    using System.Web.Configuration;

    using Exceptionless;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure our ExceptionLess Client
            ExceptionlessClient.Default.Configuration.ApiKey = WebConfigurationManager.AppSettings["ExceptionLess.ApiKey"];
        }
    }
}
