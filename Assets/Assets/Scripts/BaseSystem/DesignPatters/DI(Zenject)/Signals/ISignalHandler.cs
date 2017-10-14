using System;
using System.Collections.Generic;
using ModestTree;

namespace BaseSystems.DesignPatterns.DependencyInjection
{
    public interface ISignalHandler
    {
        void Execute(object[] args);
    }
}
