using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Infrastructure.Repositories
{
    public interface IPSCSRepository<T>
    {
        public Task<bool> Create(T item);
        public Task<T?> FindById(int id);
        public Task<bool> Remove(int id);
    }
}
