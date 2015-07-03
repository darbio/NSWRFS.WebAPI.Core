namespace NSWRFS.Base.Api.Tests.Controllers
{
    using System.Web.Http.Results;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NSWRFS.Base.Api.Controllers;

    [TestClass]
    public class StatusControllerTest
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
