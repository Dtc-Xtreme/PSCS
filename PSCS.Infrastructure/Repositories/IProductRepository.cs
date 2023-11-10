using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Infrastructure.Repositories
{
    public interface IProductRepository : IPSCSRepository<Product>
    {
        public IQueryable<Product> Products { get; }
    }
}
