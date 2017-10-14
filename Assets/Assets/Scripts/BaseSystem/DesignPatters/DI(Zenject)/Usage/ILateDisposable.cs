using System;

namespace BaseSystems.DesignPatterns.DependencyInjection
{
    public interface ILateDisposable
    {
        void LateDispose();
    }
}
