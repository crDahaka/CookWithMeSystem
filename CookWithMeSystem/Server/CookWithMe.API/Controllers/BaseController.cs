namespace CookWithMe.API.Controllers
{
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Web;
    using System.Web.Http;

    public class BaseController : ApiController
    {
        protected ICookWithMeSystemData Data { get; private set; }
        private User user;

        public BaseController(ICookWithMeSystemData data)
        {
            this.Data = data;
        }

        

        public ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        public string UserIdentityId
        {
            get
            {
                var user = UserManager.FindByName(User.Identity.Name);
                return user.Id;
            }
        }

        public User UserRecord
        {
            get
            {
                if (user != null)
                {
                    return user;
                }
                user = UserManager.FindByEmail(Thread.CurrentPrincipal.Identity.Name);
                return user;
            }
            set { user = value; }
        }

        public HttpResponseMessage CreateSerializedJsonResponse(HttpStatusCode code, object source)
        {
            var response = new HttpResponseMessage(code) { Content = new StringContent(JsonConvert.SerializeObject(source)) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue($"application/json");

            return response;
        }
    }
}
