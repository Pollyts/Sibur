using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft;
using System.Threading.Tasks;

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
            using (var client = new HttpClient())
            {
                HttpResponseMessage response1 = null;
                var content = new StringContent(myJson, Encoding.UTF8, "text/json");

                response1 = client.PostAsync(Url, content).Result;

                //response = await client.PostAsync(url, content);


            }
            //HttpClient client = GetClient();
            //    var response = await client.PostAsync(Url,
            //        new StringContent(myJson));

            //    if (response.StatusCode != HttpStatusCode.OK)
            //        return null;

            //    return JsonConvert.DeserializeObject<User>(
            //        await response.Content.ReadAsStringAsync());
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
