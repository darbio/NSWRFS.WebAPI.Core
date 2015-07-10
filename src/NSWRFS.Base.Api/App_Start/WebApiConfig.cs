// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   The WebAPI config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.Base.Api
{
    using System.Net.Http.Formatting;
    using System.Web.Configuration;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;

    using NSWRFS.Base.Api.App_Start;
    using NSWRFS.Base.Api.Attributes;
    using NSWRFS.Base.Api.ContentNegotiators;
    using NSWRFS.Base.Api.ContractResolvers;
    using NSWRFS.Base.Api.ExceptionLoggers;
    using NSWRFS.Base.Api.Filters;
    using NSWRFS.Base.Api.Formatters;

    /// <summary>
    /// The WebAPI config.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
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

            // Default to JSON only
            // http://www.strathweb.com/2013/06/supporting-only-json-in-asp-net-web-api-the-right-way/
            var jsonFormatter = new DefaultJsonFormatter();
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
            
            // Underscores for serialization
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new LowerCaseDelimitedPropertyNamesContractResolver('_');
        }
    }
}
