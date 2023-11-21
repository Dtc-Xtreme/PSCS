using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PSCS.AppLogic.Services;
using PSCS.Domain;
using PSCS.OrderingSystem.Models;
using static System.Collections.Specialized.BitVector32;

namespace PSCS.OrderingSystem.Controllers
{
    [Route("[controller]")]
    public class StorageController : BaseController
    {
        private readonly IApiService apiService;

        public StorageController(IHttpContextAccessor httpContextAccessor, IApiService api) : base(httpContextAccessor)
        {
            this.apiService = api;
        }

        [HttpGet]
        [HttpGet("{Search?}")]
        public async Task<IActionResult> Index(string? search)
        {
            IList<Storage>? storages = await apiService.GetAllStorages(); ;

            if (!string.IsNullOrEmpty(search)) storages = storages?.Where(c=>c.Name.ToLower().Contains(search.ToLower())).ToList();

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
            AddMultipleStoragesViewModel vm = new AddMultipleStoragesViewModel
            {
                AisleLetter = "B",
                AisleStart = 1,
                AisleEnd = 2,
                StackStart = 1,
                StackEnd = 5,
                LevelStart = 'A',
                LevelEnd = 'C',
                SectionStart = 1,
                SectionEnd = 4,
            };
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
                request.Name = request.Name.ToUpper();
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
                IList<Storage> newStorages = new List<Storage>();

                // dit werkt voor A0101A1 gangen maar niet voor 2EH01A1
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
                                        Name = request.AisleLetter.ToUpper() + aisle.ToString("D2") + stack.ToString("D2") + level.ToString().ToUpper() + section.ToString("D1"),
                                        Mloc = request.Mloc,
                                        Mix = request.Mix,
                                        Blocked = request.Blocked
                                    });
                            }
                        }
                    } 
                }

                IList<Storage>? result = null;

                result = await apiService.CreateMultipleStorages(newStorages);
                 
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
