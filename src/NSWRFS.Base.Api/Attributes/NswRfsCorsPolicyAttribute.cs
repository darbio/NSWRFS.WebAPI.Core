// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NswRfsCorsPolicyAttribute.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the NswRfsCorsPolicyAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NSWRFS.Base.Api.Attributes
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using System.Web.Cors;
    using System.Web.Http.Cors;

    /// <summary>
    /// The NSW RFS CORS policy attribute. This reads the list of allowed origins from the web.config file and adds them to the allowed origins.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class NswRfsCorsPolicyAttribute : Attribute, ICorsPolicyProvider
    {
        /// <summary>
        /// The CORS policy.
        /// </summary>
        private readonly CorsPolicy corsPolicy;

        /// <summary>
        /// Initializes a new instance of the <see cref="NswRfsCorsPolicyAttribute"/> class.
        /// </summary>
        public NswRfsCorsPolicyAttribute()
        {
            // Create a CORS policy.
            this.corsPolicy = new CorsPolicy()
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };
#if DEBUG
            // This allows CORS from all origins
            // This is only compiled into the app in DEBUG mode
            this.corsPolicy.AllowAnyOrigin = true;
#else
            // Add allowed origins from Web.Config.
            var allowedOrigins = WebConfigurationManager.AppSettings["NSWRFS.CorsAllowedOrigins"];

            if (!string.IsNullOrEmpty(allowedOrigins))
            {
                foreach (var origin in allowedOrigins.Split(',').Where(a => !String.IsNullOrEmpty(a)))
                {
                    this.corsPolicy.Origins.Add(origin);
                }
            }
#endif
        }

        /// <summary>
        /// The get CORS policy async.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(this.corsPolicy);
        }
    }
}