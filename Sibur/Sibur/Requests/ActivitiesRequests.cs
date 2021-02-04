using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Sibur.Models;
using System.Net;

namespace Sibur.Requests
{
    class ActivitiesRequests
    {
        private const string Url = "https://dbgrpprj.azurewebsites.net/Activities";
        private const string UrlCat = "https://dbgrpprj.azurewebsites.net/Categories";        
        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<bool> Add(ActWithCatPost act)
        {            
            HttpClient client = GetClient();
            var response = await client.PostAsync("https://dbgrpprj.azurewebsites.net/Activities/WithCat",
                new StringContent(
                    JsonConvert.SerializeObject(act),
                    Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return false;
            else
                return true;
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(UrlCat);
            return JsonConvert.DeserializeObject<IEnumerable<Category>>(result);
        }
        public async Task<IEnumerable<ActWithCatGet>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<ActWithCatGet>>(result);
        }        
    }
}
