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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("AddStorage")]
        public IActionResult AddStorage(StorageRequest request)
        {
            if (ModelState.IsValid)
            {
                return Redirect("Add");
            }
            return View("Add", request);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
