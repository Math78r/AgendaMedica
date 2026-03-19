using Microsoft.AspNetCore.Mvc;

namespace Appi_AgendaMedica.Controllers
{
    public class HistorialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
