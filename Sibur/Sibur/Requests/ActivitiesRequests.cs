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
        private const string UrlChats = "https://dbgrpprj.azurewebsites.net/ActChats";
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
        public async Task<bool> SignUp(int ActivityId, int Userid)
        {
            HttpClient client = GetClient();
            string requesturi = Url + $"/{ActivityId}?userid={Userid}";
            var response = await client.GetAsync(requesturi);
            if (response.StatusCode != HttpStatusCode.OK)
                return false;
            else
                return true;
        }

        //Удаление посещения Activities/DelAttending/id(айди мероприятия вместо id)?userid=айди юзера
        public async Task<bool> SignOut(int ActivityId, int Userid)
        {
            HttpClient client = GetClient();
            string requesturi = Url + $"/DelAttending/{ActivityId}?userid={Userid}";
            var response = await client.GetAsync(requesturi);
            if (response.StatusCode != HttpStatusCode.OK)
                return false;
            else
                return true;
        }
        public async Task<bool> AddComment(int Activityid, int Userid, string Comment)
        {
            HttpClient client = GetClient();
            ActChat ac = new ActChat() { UserId = Userid, ActivityId=Activityid, Text=Comment };
            var response = await client.PostAsync(UrlChats,
                new StringContent(
                    JsonConvert.SerializeObject(ac),
                    Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.Created)
                return false;
            else
                return true;
        }
        public async Task<IEnumerable<ActChat>> GetComments(int Activityid)
        {
            HttpClient client = GetClient();
            string requesturi = UrlChats + $"/{Activityid}";
            string result = await client.GetStringAsync(requesturi);
            return JsonConvert.DeserializeObject<IEnumerable<ActChat>>(result);
        }
    }
}
