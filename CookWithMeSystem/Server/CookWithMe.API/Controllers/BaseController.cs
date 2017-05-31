namespace CookWithMe.API.Controllers
{
    using CookWithMeSystem.Data;
    using System.Web.Http;

    public class BaseController : ApiController
    {
        protected ICookWithMeSystemData Data { get; private set; }

        public BaseController(ICookWithMeSystemData data)
        {
            this.Data = data;
        }
    }
}