namespace CookWithMe.API.Controllers
{
    using CookWithMeSystem.Data;
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;

    public class BaseController : ApiController
    {
        protected ICookWithMeSystemData Data { get; private set; }

        public BaseController(ICookWithMeSystemData data)
        {
            this.Data = data;
        }

        public HttpResponseMessage CreateSerializedJsonResponse(HttpStatusCode code, object source)
        {
            var response = new HttpResponseMessage(code) { Content = new StringContent(JsonConvert.SerializeObject(source)) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue($"application/json");

            return response;
        }
    }
}
