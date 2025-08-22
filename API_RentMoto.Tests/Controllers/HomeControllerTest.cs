using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API_RentMoto;
using API_RentMoto.Controllers;

namespace API_RentMoto.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Organizar
            HomeController controller = new HomeController();

            // Agir
            ViewResult result = controller.Index() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
