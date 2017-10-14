namespace BaseSystems.DesignPatterns.DependencyInjection
{
    public interface IBindingFinalizer
    {
        bool CopyIntoAllSubContainers
        {
            get;
        }

        void FinalizeBinding(DiContainer container);
    }
}
