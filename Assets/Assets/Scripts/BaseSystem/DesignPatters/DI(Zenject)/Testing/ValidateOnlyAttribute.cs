using System;

namespace BaseSystems.DesignPatterns.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateOnlyAttribute : Attribute
    {
    }
}


