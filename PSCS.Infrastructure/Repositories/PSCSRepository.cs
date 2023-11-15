using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Infrastructure.Repositories
{
    public class PSCSRepository<T>
    {
        protected PSCSDbContext context;

        public PSCSRepository(PSCSDbContext ctx)
        {
            this.context = ctx;
        }

        public async Task<T?> Create(T item)
        {
            if (item != null) {
                context.Add(item);
                if (await context.SaveChangesAsync() != 0)
                {
                    return item;
                }
            }

            return default;
        }

        public async Task<T?> Save(T item)
        {
            if (item != null)
            {
                if (await context.SaveChangesAsync() != 0)
                {
                    return item;
                }
            }

            return default;
        }
    }
}
