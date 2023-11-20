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
    public class SupplierRepository : ISupplierRepository
    {
        private PSCSDbContext context;

        public SupplierRepository(PSCSDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<Supplier> Suppliers => context.Suppliers;

        public async Task<bool> Create(Supplier item)
        {
            await context.Suppliers.AddAsync(item);
            return await context.SaveChangesAsync() == 0 ? false : true;
        }

        public async Task<Supplier?> FindById(int id)
        {
            return await Suppliers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> Update(Supplier supplier)
        {
            Supplier? selected = await context.Suppliers.FirstOrDefaultAsync(c => c.Id == supplier.Id);
            if (selected != null)
            {
                selected.Name = supplier.Name;
                selected.Number = supplier.Number;
                selected.Address = supplier.Address;
                selected.Phone = supplier.Phone;
                return await context.SaveChangesAsync() == 0 ? false : true;
            }
            return false;
        }

        public async Task<bool> Remove(int id)
        {
            Supplier? supplier = await Suppliers.FirstOrDefaultAsync(c => c.Id == id);
            if (supplier != null) context.Suppliers.Remove(supplier);
            context.Suppliers.Remove(supplier);
            return await context.SaveChangesAsync() == 0 ? false : true;
        }
    }
}
