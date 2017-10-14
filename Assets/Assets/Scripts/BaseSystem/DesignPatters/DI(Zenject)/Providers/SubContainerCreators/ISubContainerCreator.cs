using System.Collections.Generic;

namespace BaseSystems.DesignPatterns.DependencyInjection
{
    public interface ISubContainerCreator
    {
        DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context);
    }
}
