
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
            return this.Created(new { foo = "bar" }, new Uri("http://localhost/Test/1"));
        }
    }
}
