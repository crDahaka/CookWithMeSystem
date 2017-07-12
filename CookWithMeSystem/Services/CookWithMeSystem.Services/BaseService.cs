namespace CookWithMeSystem.Services
{
    using CookWithMeSystem.Data;

    public abstract class BaseService
    {
        protected ICookWithMeSystemData Data { get; private set; }

        public BaseService(ICookWithMeSystemData data)
        {
            this.Data = data;
        }
    }
}
