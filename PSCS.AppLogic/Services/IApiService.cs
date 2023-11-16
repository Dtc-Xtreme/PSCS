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
        public Task<List<Storage>?> GetAllStorages();
        public Task<Storage?> FindStorageById(int id);
        public Task<Storage?> SaveStorage(Storage storage);
        public Task<bool> RemoveStorage(int id);

    }
}