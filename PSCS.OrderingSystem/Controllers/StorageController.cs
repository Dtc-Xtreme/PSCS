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
        [HttpGet("{Search?}")]
        public async Task<IActionResult> Index(string? search)
        {
            IList<Storage>? storages = await apiService.GetAllStorages(); ;

            if (!string.IsNullOrEmpty(search)) storages = storages?.Where(c=>c.Name.Contains(search)).ToList();

            StorageSearchViewModel vm = new StorageSearchViewModel
            {
                Storages = storages,
                Search = search
            };

            return View(vm);
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
