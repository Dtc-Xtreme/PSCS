using Microsoft.AspNetCore.Mvc;
using PSCS.AppLogic.Services;
using PSCS.Domain;
using PSCS.OrderingSystem.Models;

namespace PSCS.OrderingSystem.Controllers
{
    [Route("[controller]")]
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

        [HttpPost("Save")]
        public async Task<IActionResult> Save(Storage request)
        {
            if (ModelState.IsValid)
            {
                Storage? result = await apiService.SaveStorage(request);
                if (result != null) return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", request);
        }

        [HttpGet("Edit")]
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Storage? selectedStorage;

            if (id == 0)
            {
                selectedStorage = new();
            }
            else
            {
                selectedStorage = await apiService.FindStorageById(id);
            }
            

            return View(selectedStorage);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await apiService.RemoveStorage(id);

            return RedirectToAction("Index");
        }
    }
}
