using System;
using System.Collections.Generic;
using ModestTree;

namespace BaseSystems.DesignPatterns.DependencyInjection
{
    public class FactoryToChoiceBinder<TParam1, TParam2, TParam3, TParam4, TContract> : FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract>
    {
        public FactoryToChoiceBinder(
            BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
            : base(bindInfo, factoryBindInfo)
        {
        }

        // Note that this is the default, so not necessary to call
        public FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TContract> ToSelf()
        {
            Assert.IsEqual(BindInfo.ToChoice, ToChoices.Self);
            return this;
        }

        public FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TConcrete> To<TConcrete>()
            where TConcrete : TContract
        {
            BindInfo.ToChoice = ToChoices.Concrete;
            BindInfo.ToTypes = new List<Type>()
            {
                typeof(TConcrete)
            };

            return new FactoryFromBinder<TParam1, TParam2, TParam3, TParam4, TConcrete>(BindInfo, FactoryBindInfo);
        }
    }
}
