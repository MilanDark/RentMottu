
using System.Web.Mvc;

namespace API_RentMoto.Controllers
{
    /// <summary>Página de informação do Sistema</summary>
    public class HomeController : Controller
    {

        /// <summary>Informações sobre o Projeto RentMotorcycle</summary>
        public ActionResult Index()
        {
            return View();
        }
    }



}
