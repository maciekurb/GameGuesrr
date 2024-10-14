namespace GameGuessr.Api.Infrastructure.DepedencyInjection
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class InjectableAttribute : Attribute
    {
        public Lifetime Lifetime { get; set; }
        public bool AllowManyImplementations { get; set; }

        public InjectableAttribute()
        {
            AllowManyImplementations = false;
            Lifetime = Lifetime.Transient;
        }
    }
}
