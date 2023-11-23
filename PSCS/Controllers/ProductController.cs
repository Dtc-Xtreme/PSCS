using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PSCS.API.Models;
using PSCS.Domain;
using PSCS.Infrastructure.Repositories;

namespace PSCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository repository)
        {
            this.productRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<Product> products = await productRepository.Products.ToListAsync();
            return Ok(products == null ? NotFound() : products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            Product? product = await productRepository.FindById(id);

            return Ok(product == null ? BadRequest() : product);
        }

        [HttpGet("FindAllByNameOrId/{search}")]
        public async Task<IActionResult> FindAllByNameOrId(string search)
        {
            IList<Product>? products = await productRepository.FindAllByNameOrId(search);
            return Ok(products == null ? NotFound() : products);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            return Ok(await productRepository.Update(product) == false ? BadRequest() : product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok(await productRepository.Remove(id) == false ? BadRequest() : "Product is removed!");
        }
    }
}
