
namespace NocInjector
{
    internal abstract class LifetimeImplementation
    {
        /// <summary>
        /// Dependency belonging to this lifetime
        /// </summary>
        protected IDependency LinkedDependency { get; }
        protected DependencyInjector Injector { get; }

        protected LifetimeImplementation(IDependency linkedDependency, DependencyInjector injector)
        {
            LinkedDependency = linkedDependency;
            Injector = injector;
        }
        
        /// <summary>
        /// Gets an instance of the dependency
        /// </summary>
        /// <returns></returns>
        public abstract object GetInstance();
    }
}