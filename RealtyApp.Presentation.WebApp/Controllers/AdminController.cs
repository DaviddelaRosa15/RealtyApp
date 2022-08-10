using Microsoft.AspNetCore.Mvc;

namespace RealtyApp.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
