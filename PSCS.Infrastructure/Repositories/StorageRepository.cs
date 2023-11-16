using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Storage = PSCS.Domain.Storage;

namespace PSCS.Infrastructure.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private PSCSDbContext context;

        public StorageRepository(PSCSDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<Storage> Storages => context.Storages;

        public async Task<bool> Create(Storage item)
        {
            await context.Storages.AddAsync(item);
            return await context.SaveChangesAsync() == 0 ? false : true;
        }

        public async Task<Storage?> FindById(int id)
        {
            return await Storages.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> Update(Storage storage)
        {
            Storage? selected = await context.Storages.FirstOrDefaultAsync(c => c.Id == storage.Id);
            if (selected != null)
            {
                selected.Mloc = storage.Mloc;
                selected.Name = storage.Name;
                selected.Mloc = storage.Mloc;
                selected.Mix = storage.Mix;
                selected.Blocked = storage.Blocked;
                return await context.SaveChangesAsync() == 0 ? false : true;
            }
            return false;
        }

        public async Task<bool> Remove(int id)
        {
            Storage? storage = await Storages.FirstOrDefaultAsync(c => c.Id == id);
            if (storage != null) context.Storages.Remove(storage);
            context.Storages.Remove(storage);
            return await context.SaveChangesAsync() == 0 ? false : true;
        }
    }
}
