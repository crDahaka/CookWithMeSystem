namespace CookWithMe.API.Models.Steps
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Models;
    using System.ComponentModel.DataAnnotations;

    public class StepViewModel : IMapFrom<Step>
    {
        public int ID { get; set; }

        [StringLength(ValidationConstants.MaxStepAction, ErrorMessage = ValidationConstants.ValidationErrorMessage, MinimumLength = ValidationConstants.MinStepAction)]
        public string Action { get; set; }
        
        [Range(ValidationConstants.MinStepTime, ValidationConstants.MaxStepTime, ErrorMessage = ValidationConstants.ValidationErrorMessage)]
        public int EstimatedTime { get; set; }

    }
}