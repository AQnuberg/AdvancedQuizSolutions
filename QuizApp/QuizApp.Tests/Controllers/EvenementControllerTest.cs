using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizApp.Controllers;

namespace QuizApp.Tests.Controllers
{
    [TestClass]
    public class EvenementControllerTest
    {
        [TestMethod]
        public void Index()
        {
            EvenementController controller = new EvenementController();
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
