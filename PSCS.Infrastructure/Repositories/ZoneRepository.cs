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
    public class ZoneRepository : IZoneRepository
    {
        private PSCSDbContext context;

        public ZoneRepository(PSCSDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<Zone> Zones => context.Zones;

        public async Task<bool> Create(Zone item)
        {
            await context.Zones.AddAsync(item);
            return await context.SaveChangesAsync() == 0 ? false : true;
        }

        public async Task<Zone?> FindById(int id)
        {
            return await Zones.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> Update(Zone zone)
        {
            Zone? selected = await context.Zones.FirstOrDefaultAsync(c => c.Id == zone.Id);
            if (selected != null)
            {
                selected.Name = zone.Name;
                return await context.SaveChangesAsync() == 0 ? false : true;
            }
            return false;
        }

        public async Task<bool> Remove(int id)
        {
            Zone? zone = await Zones.FirstOrDefaultAsync(c => c.Id == id);
            if (zone != null) context.Zones.Remove(zone);
            context.Zones.Remove(zone);
            return await context.SaveChangesAsync() == 0 ? false : true;
        }
    }
}
