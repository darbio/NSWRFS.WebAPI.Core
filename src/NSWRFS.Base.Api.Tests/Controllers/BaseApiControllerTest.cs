namespace NSWRFS.Base.Api.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Results;
    using System.Web.Mvc;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NSWRFS.Base.Api.Controllers;
    using NSWRFS.Base.Api.Models;
    using NSWRFS.Base.Api.Results;
    using NSWRFS.Base.Api.Tests.Mocks;

    [TestClass]
    public class BaseApiControllerTest
    {
        [TestMethod]
        public void TestGetListHeaders()
        {
            // Arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "ListTest/",
                new
                {
                    controller = "ListTest",
                    action = "Get"
                });

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/ListTest?page=2&per_page=2"))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.Headers.GetValues("X-Total-Page-Count").Single(), "7");
                    Assert.AreEqual(response.Headers.GetValues("X-Total-Count").Single(), "2");
                    Assert.AreEqual(response.Headers.GetValues("X-Current-Page").Single(), "2");
                    Assert.AreEqual(response.Headers.GetValues("Link").Single(), "<http://localhost/ListTest/?page=1&per_page=2>; rel=\"first\", <http://localhost/ListTest/?page=1&per_page=2>; rel=\"previous\", <http://localhost/ListTest/?page=3&per_page=2>; rel=\"next\", <http://localhost/ListTest/?page=7&per_page=2>; rel=\"last\"");
                }
            }
        }
    }
}
