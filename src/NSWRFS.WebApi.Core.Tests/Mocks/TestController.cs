
namespace NSWRFS.WebAPI.Core.Tests.Mocks
{
    using NSWRFS.WebAPI.Core.Controllers;
    using NSWRFS.WebAPI.Core.Exceptions;
    using NSWRFS.WebAPI.Core.Filters;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.ModelBinding;
    using System.Web.UI;

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
            // The entire list is sent to the OkList method and filtered in there
            var list = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...".Split(' ').ToList();

            // Return
            return this.OkList(list, new Uri("http://localhost/Test/"), page, per_page);
        }

        [Route]
        [HttpGet]
        public IHttpActionResult Get_Action(int page = 1, int per_page = 20)
        {
            // 14 items in a list
            // These are pre-split as if they have come back from a service
            var list = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...".Split(' ').Skip(2).Take(2).ToList();

            // Return
            return this.OkList(
                list,
                new Uri("http://localhost/Test/?page=1&per_page=2"),
                new Uri("http://localhost/Test/?page=1&per_page=2"),
                new Uri("http://localhost/Test/?page=3&per_page=2"),
                new Uri("http://localhost/Test/?page=7&per_page=2"),
                2,
                7);
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

        //[Route]
        //[HttpGet]
        //public IHttpActionResult GetPeopleSort_Action([QueryString]int page = 1, [QueryString]int per_page = 20, [QueryString]string sort = null)
        //{
        //    // Get our items from the service
        //    var list = this.GetPeopleWithQuery(a => a.LastName.StartsWith("S"), b => b.DateOfBirth, page, per_page);

        //    // Sort me
        //    Func<string, object> initialSort = (a, b) => { a =; };

        //    // Parse the sort string
        //    if (!string.IsNullOrEmpty(sort))
        //    {
        //        var sortTerms = sort.Split(',').ToList();
        //        foreach (var sortTerm in sortTerms)
        //        {
        //            if (sortTerm.StartsWith("-"))
        //            {
        //                // Reverse the order
        //                list.OrderByDescending(a => a.FirstName).ThenBy();
        //            }
        //            else
        //            {
        //                // In order
        //            }
        //        }
        //    }

        //    // Return
        //    return this.OkList(
        //        list,
        //        new Uri("http://localhost/Test/?page=1&per_page=2"),
        //        new Uri("http://localhost/Test/?page=1&per_page=2"),
        //        new Uri("http://localhost/Test/?page=3&per_page=2"),
        //        new Uri("http://localhost/Test/?page=7&per_page=2"),
        //        2,
        //        7);
        //}

        //private List<PersonViewModel_POST> GetPeopleWithQuery(Func<PersonViewModel_POST, bool> query, Func<PersonViewModel_POST, object> sortFunc, int page, int perPage)
        //{
        //    var list = new List<PersonViewModel_POST>()
        //    {
        //        new PersonViewModel_POST() { FirstName = "John", LastName = "Smith", EmailAddress = "john@test.com", DateOfBirth = new DateTime(1974, 1, 1) },
        //        new PersonViewModel_POST() { FirstName = "Jane", LastName = "Doe", EmailAddress = "jane@test.com", DateOfBirth = new DateTime(1966, 9, 1) },
        //        new PersonViewModel_POST() { FirstName = "Brahma", LastName = "Singh", EmailAddress = "brahma@test.com", DateOfBirth = new DateTime(1989, 4, 8) }
        //    };

        //    var skip = (perPage * page) - perPage;
        //    return list.Where(query).OrderBy(sortFunc).Skip(skip).Take(perPage).ToList();
        //}
    }
}
