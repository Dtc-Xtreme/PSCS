using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Infrastructure.Repositories
{
    public interface IStorageRepository : IPSCSRepository<Storage>
    {
        public IQueryable<Storage> Storages { get; }
        public Task<bool> Update(Storage item);
        public Task<bool> CreateMultipe(IList<Storage> storages);

    }
}
