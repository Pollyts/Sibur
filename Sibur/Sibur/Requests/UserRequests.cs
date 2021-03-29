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
    public class UserRequests
    {
        private const string Url = "https://dbgrpprj.azurewebsites.net/Users";
        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<bool> AddImage(UserImg userimg)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url+ "/Pic",
                new StringContent(
                    JsonConvert.SerializeObject(userimg),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return false;
            else
                return true;
        }

        public async Task<bool> Delete(int userId)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + $"/{userId}");
            if (response.StatusCode != HttpStatusCode.OK)
                return false;
            else
                return true;
        }
        public async Task<bool> Edit(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(Url + $"/{user.Id}",
                new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.NoContent)
                return false;
            else
                return true;
        }
        public async Task<User> Add(User usr)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(usr),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());
        }        
        public async Task<User> Entry(string mail, string password)
        {
            HttpClient client = GetClient();
            string requesturi = Url + $"?mail={mail}&pass={password}";
            var response = await client.GetAsync(requesturi);
            //if (response.RequestMessage.Content.ToString()== "Нужно подтверждение почты")
            //{
                
            //}
            return JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());
        }
        public async Task<bool> MakeAdmin(int userid)
        {
            HttpClient client = GetClient();
            string requesturi = Url + $"/Admin/{userid}";
            var response = await client.GetAsync(requesturi);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else return false;
        }
    }
}
