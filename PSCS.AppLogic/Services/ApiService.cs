using Microsoft.Extensions.Configuration;
using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.AppLogic.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration configuration;

        private HttpClient client = new HttpClient();
        private string? url;

        public ApiService(IConfiguration configuration)
        {
            this.url = configuration.GetConnectionString("api");
        }

        public async Task<IList<Storage>?> GetAllStorages()
        {
            try
            {
                return await client.GetFromJsonAsync<List<Storage>>(url + "/Storage");

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Storage?> FindStorageById(int id)
        {
            try
            {
                return await client.GetFromJsonAsync<Storage>(url + "/Storage/" + id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Storage?> SaveStorage(Storage storage)
        {
            try
            {
                HttpResponseMessage httpResponseMessage;

                if (storage.Id == 0)
                {
                    httpResponseMessage = await client.PostAsJsonAsync(url + "/Storage", storage);
                }
                else
                {
                    httpResponseMessage = await client.PutAsJsonAsync(url + "/Storage", storage);
                }

                Storage? result = httpResponseMessage.Content.ReadFromJsonAsync<Storage>().Result;
                return httpResponseMessage.StatusCode == HttpStatusCode.OK ? result : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IList<Storage>?> CreateMultipleStorages(IList<Storage> storages)
        {
            try
            {
                HttpResponseMessage httpResponseMessage;

                httpResponseMessage = await client.PostAsJsonAsync(url + "/Storage/CreateMultiple", storages);
                IList<Storage>? result = httpResponseMessage.Content.ReadFromJsonAsync<IList<Storage>>().Result;
                return httpResponseMessage.StatusCode == HttpStatusCode.OK ? result : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> RemoveStorage(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url + "/Storage/" + id);
                return response.StatusCode == System.Net.HttpStatusCode.OK ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IList<Zone>?> GetAllZones()
        {
            try
            {
                return await client.GetFromJsonAsync<List<Zone>>(url + "/Zone");

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Zone?> FindZoneById(int id)
        {
            try
            {
                return await client.GetFromJsonAsync<Zone>(url + "/Zone/" + id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Zone?> SaveZone(Zone zone)
        {
            try
            {
                HttpResponseMessage httpResponseMessage;

                if (zone.Id == 0)
                {
                    httpResponseMessage = await client.PostAsJsonAsync(url + "/Zone", zone);
                }
                else
                {
                    httpResponseMessage = await client.PutAsJsonAsync(url + "/Zone", zone);
                }

                Zone? result = httpResponseMessage.Content.ReadFromJsonAsync<Zone>().Result;
                return httpResponseMessage.StatusCode == HttpStatusCode.OK ? result : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> RemoveZone(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url + "/Zone/" + id);
                return response.StatusCode == System.Net.HttpStatusCode.OK ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<IList<Product>?> GetAllProducts()
        {
            try
            {
                return await client.GetFromJsonAsync<List<Product>>(url + "/Product");

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Product?> FindProductById(int id)
        {
            try
            {
                return await client.GetFromJsonAsync<Product>(url + "/Product/" + id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Product?> SaveProduct(Product product)
        {
            try
            {
                HttpResponseMessage httpResponseMessage;

                if (product.Id == 0)
                {
                    httpResponseMessage = await client.PostAsJsonAsync(url + "/Product", product);
                }
                else
                {
                    httpResponseMessage = await client.PutAsJsonAsync(url + "/Product", product);
                }

                Product? result = httpResponseMessage.Content.ReadFromJsonAsync<Product>().Result;
                return httpResponseMessage.StatusCode == HttpStatusCode.OK ? result : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> RemoveProduct(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url + "/Product/" + id);
                return response.StatusCode == System.Net.HttpStatusCode.OK ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IList<Supplier>?> GetAllSuppliers()
        {
            try
            {
                return await client.GetFromJsonAsync<List<Supplier>>(url + "/Supplier");

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Supplier?> FindSupplierById(int id)
        {
            try
            {
                return await client.GetFromJsonAsync<Supplier>(url + "/Supplier/" + id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
