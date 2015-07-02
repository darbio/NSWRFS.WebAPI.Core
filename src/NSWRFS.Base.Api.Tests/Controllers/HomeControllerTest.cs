using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSWRFS.Base.Api;
using NSWRFS.Base.Api.Controllers;

namespace NSWRFS.Base.Api.Tests.Controllers
{
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
            IHttpActionResult result = controller.Ping();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Health()
        {
            // Arrange
            var controller = new StatusController();

            // Act
            IHttpActionResult result = controller.Health();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
