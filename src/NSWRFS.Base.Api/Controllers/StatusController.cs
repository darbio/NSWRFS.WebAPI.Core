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

    /// <summary>
    /// The status controller.
    /// </summary>
    [RoutePrefix("api")]
    public class StatusController : ApiController
    {
        [Route("ping")]
        [HttpGet]
        public IHttpActionResult Ping()
        {
            return this.Ok("OK");
        }

        [Route("health")]
        [HttpGet]
        public IHttpActionResult Health()
        {
            // Do a test to ensure I'm healthy
            var health = new object();

            // Return the health object
            // 200 on OK
            return this.Ok(health);
        }
    }
}
