using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace LanguageTranslatorConsole
{
    public class WatsonHttpService : IWatsonHttpService
    {
        public WatsonHttpResponse Post(string path, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://watson-api-explorer.mybluemix.net");

            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string requestAsJsonString = JsonConvert.SerializeObject(data,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            HttpContent content = new StringContent(requestAsJsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(path, content).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();

                return new WatsonHttpResponse
                {
                    Status = WatsonHttpStatusCode.Ok,
                    Result = jsonString.Result
                };
            }
            else
            {
                return new WatsonHttpResponse
                {
                    Status = WatsonHttpStatusCode.Error,
                    Result = String.Empty
                };
            }
        }
    }
}