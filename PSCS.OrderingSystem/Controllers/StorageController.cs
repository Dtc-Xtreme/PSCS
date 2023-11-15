using Microsoft.AspNetCore.Mvc;
using PSCS.OrderingSystem.Models;

namespace PSCS.OrderingSystem.Controllers
{
    public class StorageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult AddStorage(StorageRequest request)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View("Index", request);
        }
    }
}
