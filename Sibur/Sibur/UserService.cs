using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Sibur.Models;
using System.Net;

namespace Sibur
{
    class UserService
    {
        private const string Url = "https://dbgrpprj.azurewebsites.net/Users";
        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        // получаем всех друзей
        //public async Task <IEnumerable<User>> Get()
        //{
        //    HttpClient client = GetClient();
        //    var content = await client.GetStringAsync(Url);
        //    return JsonConvert.DeserializeObject<IEnumerable<User>>(content);

        //}

        // добавляем одного друга

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
        public async Task Add()
        {           
            string myJson = @"{
        ""Mail"": ""123456"",
        ""Password"": ""qwerty"",
        ""MailConfirm"": true,
        ""Name"": ""qqqq"",
        ""Currency"": 0,
        ""Thanks"": 0,
        ""Role"": null,
        ""EngPoints"": 0,
        ""ActAttendings"": [],
        ""ActChat"": null,
        ""QuestTaskUsers"": [],
        ""UserImg"": [],
        ""UserQuests"": []
}";
            

                //HttpClient client = GetClient();
                //var response = await client.PostAsync(Url,
                //    new StringContent(myJson, Encoding.UTF8, "text/json"));

                //if (response.StatusCode != HttpStatusCode.OK)
                //    return null;

                //return JsonConvert.DeserializeObject<Friend>(
                //    await response.Content.ReadAsStringAsync());
            }
        //// обновляем друга
        //public async Task<User> Update(User friend)
        //{
        //    HttpClient client = GetClient();
        //    var response = await client.PutAsync(Url + '/' + friend.Id,
        //        new StringContent(
        //            JsonConvert.SerializeObject(friend),
        //            Encoding.UTF8, 'application/json'));

        //    if (response.StatusCode != HttpStatusCode.OK)
        //        return null;

        //    return JsonConvert.DeserializeObject<User>(
        //        await response.Content.ReadAsStringAsync());
        //}
        //// удаляем друга
        //public async Task<User> Delete(int id)
        //{
        //    HttpClient client = GetClient();
        //    var response = await client.DeleteAsync(Url + '/' + id);
        //    if (response.StatusCode != HttpStatusCode.OK)
        //        return null;

        //    return JsonConvert.DeserializeObject<User>(
        //       await response.Content.ReadAsStringAsync());
        //}
    }
}
