namespace CookWithMe.API.Models.Steps
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Models;
    using System.ComponentModel.DataAnnotations;

    public class StepViewModel : IMapFrom<Step>
    {
        public int ID { get; set; }

        [StringLength(ValidationConstants.MaxStepAction, ErrorMessage = ValidationConstants.RecipeErrorMessage, MinimumLength = ValidationConstants.MinStepAction)]
        public string Action { get; set; }
        
        [Range(ValidationConstants.MinStepTime, ValidationConstants.MaxStepTime, ErrorMessage = ValidationConstants.RecipeErrorMessage)]
        public int EstimatedTime { get; set; }

    }
}