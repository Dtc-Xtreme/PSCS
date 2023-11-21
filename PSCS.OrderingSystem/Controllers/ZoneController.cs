using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PSCS.AppLogic.Services;
using PSCS.Domain;
using PSCS.OrderingSystem.Models;
using static System.Collections.Specialized.BitVector32;

namespace PSCS.OrderingSystem.Controllers
{
    [Route("[controller]")]
    public class ZoneController : BaseController
    {
        private readonly IApiService apiService;

        public ZoneController(IHttpContextAccessor httpContextAccessor, IApiService api) : base(httpContextAccessor)
        {
            this.apiService = api;
        }

        [HttpGet]
        [HttpGet("{Search?}")]
        public async Task<IActionResult> Index(string? search)
        {
            ViewData["Title"] = "Zones";

            IList<Zone>? zones = await apiService.GetAllZones(); ;

            if (!string.IsNullOrEmpty(search)) zones = zones?.Where(c=>c.Name.ToLower().Contains(search.ToLower())).ToList();

            ZoneSearchViewModel vm = new ZoneSearchViewModel
            {
                Zones = zones,
                Search = search
            };

            return View(vm);
        }

        [HttpGet("Add")]
        public IActionResult Add() {
            ViewData["Title"] = "Add Zone";
            return View(new Zone());
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Zone";

            Zone? selectedZone;

            if (id == 0)
            {
                selectedZone = new();
            }
            else
            {
                selectedZone = await apiService.FindZoneById(id);
            }
            

            return View(selectedZone);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save(Zone request)
        {
            if (ModelState.IsValid)
            {
                Zone? result = await apiService.SaveZone(request);
                if (result != null) return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", request);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await apiService.RemoveZone(id);

            return RedirectToAction("Index");
        }
    }
}
