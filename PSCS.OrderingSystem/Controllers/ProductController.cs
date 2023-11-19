using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using PSCS.AppLogic.Services;
using PSCS.Domain;
using PSCS.OrderingSystem.Models;

namespace PSCS.OrderingSystem.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IApiService apiService;

        public ProductController(IApiService api)
        {
            this.apiService = api;
        }

        [HttpGet]
        [HttpGet("{Search?}")]
        public async Task<IActionResult> Index(string? search)
        {
            IList<Product>? products = await apiService.GetAllProducts();

            if (!string.IsNullOrEmpty(search)) products = products?.Where(c=>c.Name.Contains(search)).ToList();

            ProductSearchViewModel vm = new ProductSearchViewModel
            {
                Products = products,
                Search = search
            };

            return View(vm);
        }

        [HttpGet("Add")]
        public async Task<IActionResult> Add() {
            IList<Supplier>? suppliers = await apiService.GetAllSuppliers();
            ViewBag.Suppliers = suppliers;

            return View(new Product());
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Product? selectedProduct;

            if (id == 0)
            {
                selectedProduct = new();
            }
            else
            {
                selectedProduct = await apiService.FindProductById(id);
            }

            IList<Supplier>? suppliers = await apiService.GetAllSuppliers();
            ViewBag.Suppliers = suppliers;

            return View(selectedProduct);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save(Product request)
        {
            // supplier ophalen en dan kijke of het valid
            if (ModelState.IsValid)
            {
                Product? result = await apiService.SaveProduct(request);
                if (result != null) return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", request);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await apiService.RemoveProduct(id);

            return RedirectToAction("Index");
        }
    }
}