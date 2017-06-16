namespace CookWithMe.API.Infrastructure.ValidationAttributes
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    public class ProcessExceptionFilterAttribute : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionContext)
        {
            if (actionContext.Exception is ProcessException)
            {
                var result = actionContext.Exception.Message;

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(result),
                    ReasonPhrase = result
                };

                actionContext.Response = response;
            }
        }
    }
}