
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
    public class ListTestController : BaseApiController
    {
        /// <summary>
        /// The get list.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="per_page">
        /// The per page.
        /// </param>
        /// <returns>
        /// A 200 OK with OkList headers
        /// </returns>
        [Route]
        [HttpGet]
        public IHttpActionResult Get(int page = 1, int per_page = 20)
        {
            // 14 items in a list
            var list = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...".Split(' ').ToList();
            
            // Return
            return this.OkList(list, new Uri("http://localhost/ListTest/"), page, per_page);
        }
    }
}
