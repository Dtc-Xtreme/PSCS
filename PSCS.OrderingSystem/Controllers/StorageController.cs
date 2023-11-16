using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PSCS.AppLogic.Services;
using PSCS.Domain;
using PSCS.OrderingSystem.Models;
using static System.Collections.Specialized.BitVector32;

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

        [HttpGet("Add")]
        public IActionResult Add() { 
            return View(new Storage());
        }

        [HttpGet("AddMultiple")]
        public IActionResult AddMultiple()
        {
            AddMultipleStoragesViewModel vm = new AddMultipleStoragesViewModel();
            //{
            //    AisleLetter =  "B",
            //    AisleStart = 1,
            //    AisleEnd  = 2,
            //    StackStart = 1,
            //    StackEnd = 5,
            //    LevelStart = 'A',
            //    LevelEnd = 'C',
            //    SectionStart = 1,
            //    SectionEnd = 4,
            //};
            return View(vm);
        }

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

        [HttpPost("SaveMultiple")]
        public async Task<IActionResult> SaveMultiple(AddMultipleStoragesViewModel request)
        {
            if (ModelState.IsValid)
            {
                List<Storage> newStorages = new List<Storage>();

                for (int aisle = request.AisleStart; aisle < request.AisleEnd; aisle++)
                {
                    for (int stack = request.StackStart; stack < request.StackEnd; stack++)
                    {
                        for (char level = request.LevelStart; level <= request.LevelEnd; level++)
                        {
                            for (int section = request.SectionStart; section < request.SectionEnd; section++)
                            {
                                newStorages.Add(
                                    new Storage
                                    {
                                        Name = request.AisleLetter + aisle.ToString("D2") + stack.ToString("D2") + level.ToString() + section.ToString("D1"),
                                        Mloc = request.Mloc,
                                        Mix = request.Mix,
                                        Blocked = request.Blocked
                                    });
                            }
                        }
                    } 
                }

                Storage? result = null;

                foreach (Storage storage in newStorages) {
                    result = await apiService.SaveStorage(storage);
                }
                 
                if (result != null) return RedirectToAction("Index");
            }

            return RedirectToAction("AddMultiple", request);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await apiService.RemoveStorage(id);

            return RedirectToAction("Index");
        }
    }
}
