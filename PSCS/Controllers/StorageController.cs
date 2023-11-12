using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSCS.API.Models;
using PSCS.Domain;
using PSCS.Infrastructure.Repositories;

namespace PSCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : Controller
    {
        private readonly IStorageRepository storageRepository;

        public StorageController(IStorageRepository repository)
        {
            this.storageRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<Storage> storages = await storageRepository.Storages.ToListAsync();
            return Ok(storages == null ? NotFound() : storages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            Storage? storage = await storageRepository.FindById(id);

            return Ok(storage == null ? NotFound() : storage);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StorageDTO storage)
        {
            Storage? newStorage = null;

            if (ModelState.IsValid)
            {
                newStorage = new Storage
                {
                    Name = storage.Name,
                    Mloc = storage.Mloc
                };
            }
            return Ok(await storageRepository.Create(newStorage) == false ? NotFound() : newStorage);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok(await storageRepository.Remove(id) == false ? NotFound() : "Supplier is removed!");
        }
    }
}
