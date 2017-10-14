using System;

namespace BaseSystems.DesignPatterns.DependencyInjection
{
    public class ValidationMarker
    {
        public ValidationMarker(Type markedType)
        {
            MarkedType = markedType;
        }

        public Type MarkedType
        {
            get;
            private set;
        }
    }
}

