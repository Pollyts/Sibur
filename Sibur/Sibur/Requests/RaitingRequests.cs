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
    public class RaitingRequests
    {
        private const string Url = "https://dbgrpprj.azurewebsites.net/Users";
        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<UserRank>> GetMonthRank()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url+"/MonthRank");
            return JsonConvert.DeserializeObject<IEnumerable<UserRank>>(result);
        }
        public async Task<IEnumerable<UserRank>> GetRank()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/Rank");
            return JsonConvert.DeserializeObject<IEnumerable<UserRank>>(result);
        }
    }
}
