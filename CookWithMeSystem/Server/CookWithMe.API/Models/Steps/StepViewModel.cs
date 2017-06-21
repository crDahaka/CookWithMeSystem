namespace CookWithMe.API.Models.Steps
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Models;

    public class StepViewModel : IMapFrom<Step>
    {
        public int ID { get; set; }

        public string Action { get; set; }

        public int EstimatedTime { get; set; }

    }
}