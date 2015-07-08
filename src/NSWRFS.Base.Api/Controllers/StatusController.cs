// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusController.cs" company="NSW RFS">
//   Copyright 2014 NSW Rural Fire Service, NSW Government, Australia
// </copyright>
// <summary>
//   Defines the StatusController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



namespace NSWRFS.Base.Api.Controllers
{
    using System.Web.Http;

    using NSWRFS.Base.Api.Exceptions;
    using NSWRFS.Base.Api.Models;

    /// <summary>
    /// The status controller.
    /// </summary>
    [RoutePrefix("api")]
    public class StatusController : ApiController
    {
        /// <summary>
        /// A HTTP ping endpoint. Always returns 200 OK if the application is up.
        /// </summary>
        /// <returns>
        /// 200 OK with content "OK".
        /// </returns>
        [Route("ping")]
        [HttpGet]
        public IHttpActionResult Ping()
        {
            return this.Ok("OK");
        }

        /// <summary>
        /// A HTTP health endpoint. Always returns 200 OK if the application is up. Content is an object which contains the health of any downstream systems upon which this API relies.
        /// </summary>
        /// <returns>
        /// 200 OK with content HealthViewModel_GET.
        /// </returns>
        [Route("health")]
        [HttpGet]
        public IHttpActionResult Health()
        {
            // Do a test to ensure I'm healthy
            var isHealthy = true; // TODO

            // Construct my return object
            var health = new HealthViewModel_GET()
            {
                IsHealthy = isHealthy
            };

            // Return the health object
            // HTTP 200 OK
            return this.Ok(health);
        }
    }
}
