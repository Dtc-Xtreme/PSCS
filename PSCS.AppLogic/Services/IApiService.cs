using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
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

        public Task<IList<Zone>?> GetAllZones();
        public Task<Zone?> FindZoneById(int id);
        public Task<Zone?> SaveZone(Zone zone);
        public Task<bool> RemoveZone(int id);

        public Task<IList<Product>?> GetAllProducts();
        public Task<Product?> FindProductById(int id);
        public Task<IList<Product>?> FindAllByNameOrId(string search);
        public Task<Product?> SaveProduct(Product product);
        public Task<bool> RemoveProduct(int id);

        public Task<IList<Supplier>?> GetAllSuppliers();
        public Task<Supplier?> FindSupplierById(int id);
        public Task<Supplier?> SaveSupplier(Supplier supplier);
        public Task<bool> RemoveSupplier(int id);

        public Task<IList<Order>?> GetAllOrders();
        public Task<Order?> SaveOrder(Order order);
    }
}
