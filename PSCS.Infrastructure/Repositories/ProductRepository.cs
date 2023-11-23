using Microsoft.EntityFrameworkCore;
using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private PSCSDbContext context;

        public ProductRepository(PSCSDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<Product> Products => context.Products.Include(c=>c.Supplier);

        public async Task<bool> Create(Product item)
        {
            await context.Products.AddAsync(item);
            return await context.SaveChangesAsync() == 0 ? false : true;
        }

        public async Task<Product?> FindById(int id)
        {
            return await Products.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Product>?> FindAllByNameOrId(string search)
        {

            if(int.TryParse(search, out int productNumber))
            {
                return await Products.Where(c => c.Number == productNumber || c.Name.ToLower().Contains(search.ToLower())).ToListAsync();
            }

            return await Products.Where(c => c.Name.ToLower().Contains(search.ToLower())).ToListAsync();
        }

        public async Task<bool> Update(Product product)
        {
            Product? selected = await context.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (selected != null)
            {
                selected.Number = product.Number;
                selected.Name = product.Name;
                selected.Description = product.Description;
                selected.SupplierId = product.SupplierId;
                if(product.Image != null) selected.Image = product.Image;
                return await context.SaveChangesAsync() == 0 ? false : true;
            }
            return false;
        }

        public async Task<bool> Remove(int id)
        {
            Product? product = await Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product != null) context.Products.Remove(product);
            context.Products.Remove(product);
            return await context.SaveChangesAsync() == 0 ? false : true;
        }
    }
}
