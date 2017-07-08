namespace CookWithMe.API.Infrastructure
{
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class JsonHelper
    {
        public static HttpResponseMessage CreateSerializedJsonResponse(HttpStatusCode code, object source)
        {
            var response = new HttpResponseMessage(code) { Content = new StringContent(JsonConvert.SerializeObject(source)) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue($"application/json");

            return response;
        }
    }
}