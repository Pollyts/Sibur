using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Sibur.Models;
using System.Net;

namespace Sibur.Requests
{
    class ActivitiesRequests
    {
        private const string Url = "https://dbgrpprj.azurewebsites.net/Activities";
        private const string UrlCat = "https://dbgrpprj.azurewebsites.net/Categories";
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<ActWithCat> Add(Activity act)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(act),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<ActWithCat>(
                await response.Content.ReadAsStringAsync(), options);
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(UrlCat);
            return JsonSerializer.Deserialize<IEnumerable<Category>>(result, options);
        }
        public async Task<IEnumerable<ActWithCatGet>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<ActWithCatGet>>(result, options);
        }        
    }
}
