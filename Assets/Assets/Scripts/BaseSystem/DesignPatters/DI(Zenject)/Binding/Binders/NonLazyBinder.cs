namespace BaseSystems.DesignPatterns.DependencyInjection
{
    public class NonLazyBinder
    {
        public NonLazyBinder(BindInfo bindInfo)
        {
            BindInfo = bindInfo;
        }

        protected BindInfo BindInfo
        {
            get;
            private set;
        }

        public void NonLazy()
        {
            BindInfo.NonLazy = true;
        }
    }
}
