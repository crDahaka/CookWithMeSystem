namespace CookWithMe.API.Controllers
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;

    using CookWithMeSystem.Models;
    using CookWithMe.API.Models.Account;
    using CookWithMe.API.Infrastructure.ValidationAttributes;

    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private IAuthenticationManager Authentication { get { return Request.GetOwinContext().Authentication; } }
        private ApplicationUserManager userManager;

        public AccountController()
        { }
       
        public AccountController(ApplicationUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { userManager = value; }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [HttpPost]
        [AllowAnonymous]
        [ValidationModelState]
        [Route("register")]
        public async Task<IHttpActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest("Invalid model body.");
            }

            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email
            };

            IdentityResult registerOperation = await UserManager.CreateAsync(newUser, model.Password);
            
            return Ok();
        }

        [Route("logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [HttpPut]
        [ValidationModelState]
        [Route("changePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest("Invalid model state.");
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            
            return Ok();
        }
        
        [HttpPost]
        [ValidationModelState]
        [Route("setPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest("Invalid model state.");
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
            
            return Ok();
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
                userManager = null;
            }

            base.Dispose(disposing);
        }
        
    }
}
