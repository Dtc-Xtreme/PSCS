using Microsoft.AspNetCore.Mvc;

namespace PSCS.OrderingSystem.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
