using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ParkMyLots.Data.Exceptions;
using ParkMyLots.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ParkMyLots.Data.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;
        public ClientService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("AccountKey", configuration["DataMallAPIKey"]);
        }

        public async Task<T> GetAsync<T>(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException();
            }
            var jsonResult = await GetJsonAsync(uri);
            return JsonConvert.DeserializeObject<T>(jsonResult);
        }

        private async Task<string> GetJsonAsync(Uri uri)
        {
            var response = await _httpClient.GetAsync(uri);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new InvalidKeyException();
            }
            return await response.Content.ReadAsStringAsync();

        }
    }
}
