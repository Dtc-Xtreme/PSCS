using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PSCS.AppLogic.Services;
using PSCS.Domain;
using PSCS.OrderingSystem.Models;
using static System.Collections.Specialized.BitVector32;

namespace PSCS.OrderingSystem.Controllers
{
    [Route("[controller]")]
    public class SupplierController : Controller
    {
        private readonly IApiService apiService;

        public SupplierController(IApiService api)
        {
            this.apiService = api;
        }

        [HttpGet]
        [HttpGet("{Search?}")]
        public async Task<IActionResult> Index(string? search)
        {
            IList<Supplier>? suppliers = await apiService.GetAllSuppliers(); ;

            if (!string.IsNullOrEmpty(search)) suppliers = suppliers?.Where(c=>c.Name.ToLower().Contains(search.ToLower())).ToList();

            SupplierSearchViewModel vm = new SupplierSearchViewModel
            {
                Suppliers = suppliers,
                Search = search
            };

            return View(vm);
        }

        [HttpGet("Add")]
        public IActionResult Add() { 
            return View(new Supplier());
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Supplier? selectedSupplier;

            if (id == 0)
            {
                selectedSupplier = new();
            }
            else
            {
                selectedSupplier = await apiService.FindSupplierById(id);
            }

            return View(selectedSupplier);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save(Supplier request)
        {
            if (ModelState.IsValid)
            {
                Supplier? result = await apiService.SaveSupplier(request);
                if (result != null) return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", request);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await apiService.RemoveSupplier(id);

            return RedirectToAction("Index");
        }
    }
}
