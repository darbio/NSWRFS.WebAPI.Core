
namespace NSWRFS.Base.Api.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.UI;

    using NSWRFS.Base.Api.Controllers;
    using NSWRFS.Base.Api.Exceptions;
    using NSWRFS.Base.Api.Filters;

    /// <summary>
    /// The list test controller.
    /// </summary>
    public class TestController : BaseApiController
    {
        [Route]
        [HttpGet]
        public IHttpActionResult Get(int page = 1, int per_page = 20)
        {
            // 14 items in a list
            var list = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...".Split(' ').ToList();
            
            // Return
            return this.OkList(list, new Uri("http://localhost/Test/"), page, per_page);
        }

        [Route]
        [HttpPost]
        public IHttpActionResult Post()
        {
            // Return
            return this.Created(new Uri("http://localhost/Test/1"), new { foo = "bar" });
        }

        [Route("NoContent")]
        [HttpPost]
        public IHttpActionResult NoContent()
        {
            // Return
            return base.NoContent();
        }

        [Route("NotModified")]
        [HttpPost]
        public IHttpActionResult NotModified()
        { 
            // Return
            return base.NotModified();
        }

        [Route("MethodNotAllowed")]
        [HttpPost]
        public IHttpActionResult MethodNotAllowed()
        {
            // Return
            return base.MethodNotAllowed();
        }

        [Route("Gone")]
        [HttpGet]
        public IHttpActionResult Gone()
        {
            // Return
            return base.Gone();
        }

        [Route("UnsupportedMediaType")]
        [HttpGet]
        public IHttpActionResult UnsupportedMediaType()
        {
            // Return
            return base.UnsupportedMediaType();
        }

        [Route("UnprocessableEntity")]
        [HttpPost]
        public IHttpActionResult UnprocessableEntity()
        {
            // Return
            return base.UnprocessibleEntity();
        }

        [Route("Exception")]
        [HttpPost]
        [ExceptionHandling]
        public IHttpActionResult Exception()
        {
            throw new Exception("This is a test exception");

            // Return
            return base.Ok();
        }

        [Route("BusinessException")]
        [HttpPost]
        [ExceptionHandling]
        public IHttpActionResult BusinessException()
        {
            throw new BusinessException("This is a test business exception");

            // Return
            return base.Ok();
        }
    }
}
