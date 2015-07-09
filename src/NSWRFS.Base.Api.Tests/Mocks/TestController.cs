
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

        [HttpPost]
        public IHttpActionResult NoContent_Action()
        {
            // Return
            return this.NoContent();
        }

        [HttpPost]
        public IHttpActionResult NotModified_Action()
        { 
            // Return
            return this.NotModified();
        }

        [HttpPost]
        public IHttpActionResult MethodNotAllowed_Action()
        {
            // Return
            return this.MethodNotAllowed();
        }

        [HttpGet]
        public IHttpActionResult Gone_Action()
        {
            // Return
            return this.Gone();
        }

        [HttpGet]
        public IHttpActionResult UnsupportedMediaType_Action()
        {
            // Return
            return this.UnsupportedMediaType();
        }

        [HttpPost]
        [ExceptionHandling]
        public IHttpActionResult Exception_Action()
        {
            throw new Exception("This is a test exception");

            // Return
            return this.Ok();
        }

        [HttpPost]
        [ExceptionHandling]
        public IHttpActionResult BusinessException_Action()
        {
            throw new BusinessException("This is a test business exception");

            // Return
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult UnprocessableEntity_Action(PersonViewModel_POST model)
        {
            if (!ModelState.IsValid)
            {
                // Send a 422 response
                return this.UnprocessableEntity();
            }

            // Return
            return this.Ok();
        }
    }
}
