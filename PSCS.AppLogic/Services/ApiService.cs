using Microsoft.Extensions.Configuration;
using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Storage>?> GetAllStorages()
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
