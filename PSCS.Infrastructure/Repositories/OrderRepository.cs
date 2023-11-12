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
    public class OrderRepository : IOrderRepsository
    {
        private PSCSDbContext context;

        public OrderRepository(PSCSDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders.Include(c=>c.Zone).Include(c=>c.OrderLines).ThenInclude(c=>c.Product).ThenInclude(c=>c.Supplier);

        public async Task<bool> Create(Order item)
        {
            await context.Orders.AddAsync(item);
            return await context.SaveChangesAsync() == 0 ? false : true;
        }

        public async Task<Order?> FindById(int id)
        {
            return await Orders.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> Remove(int id)
        {
            Order? order = await Orders.FirstOrDefaultAsync(c => c.Id == id);
            if (order != null) context.Orders.Remove(order);
            context.Orders.Remove(order);
            return await context.SaveChangesAsync() == 0 ? false : true;
        }
    }
}
