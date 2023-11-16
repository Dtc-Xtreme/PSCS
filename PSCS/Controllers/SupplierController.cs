using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSCS.API.Models;
using PSCS.Domain;
using PSCS.Infrastructure.Repositories;

namespace PSCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository supplierRepository;

        public SupplierController(ISupplierRepository repository)
        {
            this.supplierRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<Supplier> suppliers = await supplierRepository.Suppliers.ToListAsync();
            return Ok(suppliers == null ? BadRequest() : suppliers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            Supplier? supplier = await supplierRepository.FindById(id);

            return Ok(supplier == null ? BadRequest() : supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierDTO supplier)
        {
            Supplier? newSupplier = null;

            if (ModelState.IsValid)
            {
                newSupplier = new Supplier
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Address = supplier.Address,
                    Phone = supplier.Phone
                };
            }
            return Ok(await supplierRepository.Create(newSupplier) == false ? BadRequest() : newSupplier);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok(await supplierRepository.Remove(id) == false ? BadRequest() : "Supplier is removed!");
        }
    }
}
