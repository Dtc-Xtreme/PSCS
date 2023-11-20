using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Infrastructure.Repositories
{
    public interface ISupplierRepository : IPSCSRepository<Supplier>
    {
        public IQueryable<Supplier> Suppliers { get; }
        public Task<bool> Update(Supplier supplier);
    }
}
