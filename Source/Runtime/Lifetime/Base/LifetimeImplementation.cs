
namespace NocInjector
{
    public abstract class LifetimeImplementation
    {
        /// <summary>
        /// Dependency belonging to this lifetime
        /// </summary>
        internal IDependency Dependency { get; }
        
        internal IInstanceHandler InstanceHandler { get; }

        internal LifetimeImplementation(IDependency dependency, IInstanceHandler instanceHandler)
        {
            Dependency = dependency;
            InstanceHandler = instanceHandler;
        }

        /// <summary>
        /// Gets an instance of the dependency
        /// </summary>
        /// <returns></returns>
        public abstract object GetInstance();
    }
}