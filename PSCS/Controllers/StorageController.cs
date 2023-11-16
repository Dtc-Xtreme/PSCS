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
            return Ok(storages == null ? BadRequest() : storages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            Storage? storage = await storageRepository.FindById(id);

            return Ok(storage == null ? BadRequest() : storage);
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
                    Mloc = storage.Mloc,
                    Mix = storage.Mix,
                    Blocked = storage.Blocked
                };
            }
            return Ok(await storageRepository.Create(newStorage) == false ? BadRequest() : newStorage);
        }

        [HttpPost("CreateMultiple")]
        public async Task<IActionResult> CreateMultiple(List<Storage> storageList)
        {
            if (storageList == null || !storageList.Any())
            {
                return BadRequest("The provided list is empty or null.");
            }

            // Save the list of new storages to the repository
            

            return Ok(await storageRepository.CreateMultipe(storageList) == false ? BadRequest() : storageList);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Storage storage)
        {
            return Ok(await storageRepository.Update(storage) == false ? BadRequest() : storage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok(await storageRepository.Remove(id) == false ? BadRequest() : "Supplier is removed!");
        }
    }
}
