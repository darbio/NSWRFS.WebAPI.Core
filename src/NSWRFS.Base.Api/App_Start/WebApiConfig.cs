namespace NSWRFS.Base.Api
{
    using System.Linq;
    using System.Web.Configuration;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;

    using NSWRFS.Base.Api.App_Start;
    using NSWRFS.Base.Api.Attributes;
    using NSWRFS.Base.Api.ContractResolvers;
    using NSWRFS.Base.Api.ExceptionLoggers;
    using NSWRFS.Base.Api.Filters;
    using NSWRFS.Base.Api.Formatters;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Auth0 Authentication handler
            var clientId = WebConfigurationManager.AppSettings["Auth0.ClientID"];
            var clientSecret = WebConfigurationManager.AppSettings["Auth0.ClientSecret"];

            config.MessageHandlers.Add(new JsonWebTokenValidationHandler
            {
                Audience = clientId,
                SymmetricKey = clientSecret
            });

            // Add authorize to all routes
            // Unless overriddeen by AllowAnonymousAttribute on the class level
            config.Filters.Add(new AuthorizeAttribute());

            // Ad request and response logging filter
            config.Filters.Add(new LogAllActionFilterAttribute());

            // Add Exception Handling filter
            config.Filters.Add(new ExceptionHandlingAttribute());

            // Add NLog exception handler
            config.Services.Add(typeof(IExceptionLogger), new NLogExceptionLogger());

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Allow CORS access from all NSW RFS Subdomains
            config.EnableCors(new NswRfsCorsPolicyAttribute());

            // Default to JSON
            config.Formatters.Add(new DefaultJsonFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new LowerCaseDelimitedPropertyNamesContractResolver('_');
        }
    }
}
