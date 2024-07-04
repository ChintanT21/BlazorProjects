using Microsoft.AspNetCore.Mvc;

namespace BMS.Server.Controllers
{
    public class CourceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
