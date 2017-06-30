namespace CookWithMe.API.Infrastructure.ValidationAttributes
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class FinalizeValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            foreach (var element in list)
            {
                if (element != null)
                {
                    return list.Count > 0;
                }
            }
            return false;
        }
    }
}