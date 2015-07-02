using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSWRFS.Base.Api;
using NSWRFS.Base.Api.Controllers;

namespace NSWRFS.Base.Api.Tests.Controllers
{
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Results;

    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Ping()
        {
            // Arrange
            var controller = new StatusController();

            // Act
            var result = controller.Ping() as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("OK", result.Content);
        }

        [TestMethod]
        public void Health()
        {
            // Arrange
            var controller = new StatusController();

            // Act
            var result = controller.Health() as OkNegotiatedContentResult<object>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
