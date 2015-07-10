namespace NSWRFS.Base.Api.Tests.Controllers
{
    using System.Web.Http.Results;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NSWRFS.Base.Api.Controllers;
    using NSWRFS.Base.Api.Models;

    [TestClass]
    public class StatusControllerTest
    {
        [TestMethod]
        public void Ping_ReturnsOk_Always()
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
        public void Health_ReturnsOk_Always()
        {
            // Arrange
            var controller = new StatusController();

            // Act
            var result = controller.Health() as OkNegotiatedContentResult<HealthViewModel_GET>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
