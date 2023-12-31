﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using PSCS.AppLogic.Services;
using PSCS.Domain;
using PSCS.OrderingSystem.Models;
using System.IO.Compression;

namespace PSCS.OrderingSystem.Controllers
{
    [Route("[controller]")]
    public class ProductController : BaseController
    {
        private readonly IApiService apiService;

        public ProductController(IHttpContextAccessor httpContextAccessor, IApiService api) : base(httpContextAccessor)
        {
            this.apiService = api;
        }

        [HttpGet]
        [HttpGet("{Search?}")]
        public async Task<IActionResult> Index(string? search)
        {
            IList<Product>? products = await apiService.GetAllProducts();

            if (!string.IsNullOrEmpty(search)) products = products?.Where(c => c.Name.ToLower().Contains(search.ToLower()) || c.Id.ToString().ToLower().Contains(search.ToLower())).ToList();

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

            return View(new ProductAddEditViewModel());
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            ProductAddEditViewModel vm = new ProductAddEditViewModel();

            if (id > 0)
            {
                Product? selectedProduct = await apiService.FindProductById(id);
                if(selectedProduct != null)
                {
                    vm.Id = selectedProduct.Id;
                    vm.Name = selectedProduct.Name;
                    vm.Description = selectedProduct.Description;
                    if(selectedProduct.Image != null) vm.ImageBytes = selectedProduct.Image;
                }
            }

            IList<Supplier>? suppliers = await apiService.GetAllSuppliers();
            ViewBag.Suppliers = suppliers;

            return View(vm);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save(ProductAddEditViewModel vm)
        {
            // supplier ophalen en dan kijke of het valid
            if (ModelState.IsValid)
            {
                Product newProduct = new Product();
                newProduct.Id = vm.Id;
                newProduct.Number = vm.Number;
                newProduct.Name = vm.Name;
                newProduct.Description = vm.Description;
                newProduct.SupplierId = vm.SupplierId;

                if(vm.Image != null)
                {
                    // Selected file to byte[]
                    using (var memoryStream = new MemoryStream())
                    {
                        await vm.Image.CopyToAsync(memoryStream);

                        // Upload the file if less than 1,2 MB
                        if (memoryStream.Length < 1200000)
                        {
                            newProduct.Image = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }
                }

                Product? result = await apiService.SaveProduct(newProduct);
                if (result != null) return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", vm.Id);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await apiService.RemoveProduct(id);

            return RedirectToAction("Index");
        }
    }
}