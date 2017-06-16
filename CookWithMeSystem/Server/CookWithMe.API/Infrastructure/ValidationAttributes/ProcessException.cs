namespace CookWithMe.API.Infrastructure.ValidationAttributes
{
    using System;

    public class ProcessException : Exception
    {
        public ProcessException(string message)
            : base(message)
        {

        }
    }
}