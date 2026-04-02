using Microsoft.AspNetCore.Mvc;

namespace BTS.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
