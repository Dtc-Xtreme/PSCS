﻿using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.AppLogic.Services
{
    public interface IApiService
    {
        public Task<IList<Storage>?> GetAllStorages();
        public Task<Storage?> FindStorageById(int id);
        public Task<Storage?> SaveStorage(Storage storage);
        public Task<IList<Storage>?> CreateMultipleStorages(IList<Storage> storages);
        public Task<bool> RemoveStorage(int id);

    }
}
