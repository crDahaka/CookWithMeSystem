namespace CookWithMe.API.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CheckModelForNullAttribute : ActionFilterAttribute
    {
        private readonly Func<Dictionary<string, object>, bool> validate;

        public CheckModelForNullAttribute()
            : this(arguments => arguments.ContainsValue(null))
        { }

        public CheckModelForNullAttribute(Func<Dictionary<string, object>, bool> checkCondition)
        {
            validate = checkCondition;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!validate(actionContext.ActionArguments))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The argument cannot be null.");
            }
        }
    }
}