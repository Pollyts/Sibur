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
        //        public async Task Add()
        //        {           
        //            string myJson = @"{
        //        ""Mail"": ""123456"",
        //        ""Password"": ""qwerty"",
        //        ""MailConfirm"": true,
        //        ""Name"": ""qqqq"",
        //        ""Currency"": 0,
        //        ""Thanks"": 0,
        //        ""Role"": null,
        //        ""EngPoints"": 0,
        //        ""ActAttendings"": [],
        //        ""ActChat"": null,
        //        ""QuestTaskUsers"": [],
        //        ""UserImg"": [],
        //        ""UserQuests"": []
        //}";



    }
}
