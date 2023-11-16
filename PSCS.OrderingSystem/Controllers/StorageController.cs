using Microsoft.AspNetCore.Mvc;
using PSCS.AppLogic.Services;
using PSCS.Domain;
using PSCS.OrderingSystem.Models;

namespace PSCS.OrderingSystem.Controllers
{
    //[Route("[controller]/[Action]")]
    public class StorageController : Controller
    {
        private readonly IApiService apiService;

        public StorageController(IApiService api)
        {
            this.apiService = api;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IList<Storage>? storages = await apiService.GetAllStorages();
            return View(storages);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStorage(StorageRequest request)
        {
            if (ModelState.IsValid)
            {
                return Redirect("Add");
            }
            return View("Add", request);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await apiService.RemoveStorage(id);

            return RedirectToAction("Index");
        }
    }
}
