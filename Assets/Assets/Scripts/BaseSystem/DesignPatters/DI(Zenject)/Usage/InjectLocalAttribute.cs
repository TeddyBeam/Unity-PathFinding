using System;

namespace BaseSystems.DesignPatterns.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Parameter
        | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class InjectLocalAttribute : InjectAttributeBase
    {
        public InjectLocalAttribute()
        {
            Source = InjectSources.Local;
        }
    }
}
