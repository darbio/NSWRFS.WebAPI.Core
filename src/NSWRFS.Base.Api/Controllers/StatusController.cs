using System.Web.Http;

namespace NSWRFS.Base.Api.Controllers
{
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
