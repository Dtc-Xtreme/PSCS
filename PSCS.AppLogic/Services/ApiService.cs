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
    }
}
