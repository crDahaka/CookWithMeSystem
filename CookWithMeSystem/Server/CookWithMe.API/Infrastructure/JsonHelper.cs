namespace CookWithMe.API.Infrastructure
{
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http;

    public class JsonHelper
    {
        public static HttpResponseMessage CreateSerializedJsonResponse(HttpStatusCode code, object source)
        {
            var response = new HttpResponseMessage(code) { Content = new StringContent(JsonConvert.SerializeObject(source)) };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue($"application/json");

            return response;
        }
    }
}