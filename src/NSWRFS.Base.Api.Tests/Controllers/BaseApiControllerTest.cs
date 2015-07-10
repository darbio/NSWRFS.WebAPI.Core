namespace NSWRFS.Base.Api.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
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
        public void GetList_ReturnsListWithHeaders_Always()
        {
            // Arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{action}",
                new
                {
                    controller = "Test",
                    action = "Get"
                });

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Test?page=2&per_page=2"))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    Assert.AreEqual(response.Headers.GetValues("X-Total-Page-Count").Single(), "7");
                    Assert.AreEqual(response.Headers.GetValues("X-Total-Count").Single(), "2");
                    Assert.AreEqual(response.Headers.GetValues("X-Current-Page").Single(), "2");
                    Assert.AreEqual(response.Headers.GetValues("Link").Single(), "<http://localhost/Test/?page=1&per_page=2>; rel=\"first\", <http://localhost/Test/?page=1&per_page=2>; rel=\"previous\", <http://localhost/Test/?page=3&per_page=2>; rel=\"next\", <http://localhost/Test/?page=7&per_page=2>; rel=\"last\"");
                }
            }
        }

        [TestMethod]
        public void GetList_ReturnsListCountCheck_Always()
        {
            // Arrange
            var controller = new TestController();

            // Act
            var result = controller.Get(2, 2) as OkNegotiatedIListContentResult<IList<string>, string>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
            Assert.AreEqual("quisquam", result.Content[0]);
            Assert.AreEqual("est", result.Content[1]);
        }

        [TestMethod]
        public void Create_Returns201_Always()
        {
            // Arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{action}",
                new
                {
                    controller = "Test",
                    action = "Post"
                });

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/Test"))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                    Assert.AreEqual(response.Headers.Location, "http://localhost/Test/1");
                }
            }
        }

        [TestMethod]
        public void NoContent_Returns204_Always()
        {
            // Arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{action}",
                new
                {
                    controller = "Test",
                    action = "NoContent"
                });

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/Test/NoContent_Action"))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
                }
            }
        }

        [TestMethod]
        public void NotModified_Returns304_Always()
        {
            // Arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{action}",
                new
                {
                    controller = "Test",
                    action = "NotModified"
                });

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/Test/NotModified_Action"))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.NotModified);
                }
            }
        }


        [TestMethod]
        public void MethodNotAllowed_Returns405_Always()
        {
            // Arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{action}",
                new
                {
                    controller = "Test",
                    action = "MethodNotAllowed"
                });

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/Test/MethodNotAllowed_Action"))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.MethodNotAllowed);
                }
            }
        }

        [TestMethod]
        public void Gone_Returns410_Always()
        {
            // Arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{action}",
                new
                {
                    controller = "Test",
                    action = "Gone"
                });

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Test/Gone_Action"))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.Gone);
                }
            }
        }

        [TestMethod]
        public void UnsupportedMediaType_Returns415_Always()
        {
            // Arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{action}",
                new
                {
                    controller = "Test",
                    action = "UnsupportedMediaType"
                });

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Test/UnsupportedMediaType_Action"))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.UnsupportedMediaType);
                }
            }
        }

        [TestMethod]
        public void Exception_Returns500_Always()
        {
            // Arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{action}",
                new
                {
                    controller = "Test",
                    action = "Exception"
                });

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/Test/Exception_Action"))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.ReasonPhrase, "Critical Exception");
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
                }
            }
        }

        [TestMethod]
        public void BusinessException_Returns500_Always()
        {
            // Arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{action}",
                new
                {
                    controller = "Test",
                    action = "BusinessException"
                });

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/Test/BusinessException_Action"))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.ReasonPhrase, "Exception");
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
                }
            }
        }

        [TestMethod]
        public void ValidationError_Returns422_Always()
        {
            // Arrange
            var viewmodel = new PersonViewModel_POST()
            {
                FirstName = null,
                LastName = "Smith",
                EmailAddress = "www.google.com"
            };

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "{controller}/{action}");

            var server = new HttpServer(config);

            using (var client = new HttpMessageInvoker(server))
            {
                // Act
                using (var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/Test/UnprocessableEntity_Action")
                {
                    Content = new ObjectContent(typeof(PersonViewModel_POST), viewmodel, new JsonMediaTypeFormatter())
                })
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    // Assert
                    Assert.IsNotNull(response);
                    Assert.AreEqual(response.ReasonPhrase, "Unprocessable Entity");
                    Assert.AreEqual(response.StatusCode, (HttpStatusCode)422);
                }
            }
        }

        //[TestMethod]
        //public void Sort_Returns200List_Always()
        //{
        //    // Arrange
        //    var config = new HttpConfiguration();
        //    config.Routes.MapHttpRoute("Default", "{controller}/{action}");

        //    var server = new HttpServer(config);

        //    using (var client = new HttpMessageInvoker(server))
        //    {
        //        // Act
        //        using (var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Test/GetPeopleSort_Action?page=1&per_page=2&sort=-first_name,date_of_birth"))
        //        using (var response = client.SendAsync(request, CancellationToken.None).Result)
        //        {
        //            // Assert
        //            Assert.IsNotNull(response);
        //            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        //        }
        //    }
        //}
    }
}
