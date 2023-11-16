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

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            Product? newProduct = null;

            if (ModelState.IsValid)
            {
                newProduct = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    SupplierId = product.SupplierId
                };
            }
            return Ok(await productRepository.Create(newProduct) == false ? BadRequest() : newProduct);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok(await productRepository.Remove(id) == false ? BadRequest() : "Product is removed!");
        }
    }
}
