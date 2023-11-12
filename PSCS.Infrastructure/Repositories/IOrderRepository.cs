using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Infrastructure.Repositories
{
    public interface IOrderRepsository : IPSCSRepository<Order>
    {
        public IQueryable<Order> Orders { get; }
    }
}
