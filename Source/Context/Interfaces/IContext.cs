namespace NocInjector
{
    /// <summary>
    /// Context base interface
    /// </summary>
    public interface IContext
    {
        public IDependencyContainer Container { get; }
    }
}