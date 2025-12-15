using System;

namespace NocInjector
{
    /// <summary>
    /// A composition that combines all the modules of a container
    /// </summary>
    internal sealed class DependencyContainer : IDependencyContainer
    {
        private readonly IDependencyContainer _parentContainer;
        
        private readonly DependenciesResolver _resolver;
        
        internal DependencyContainer(IDependenciesStorage storage, IDependencyContainer parentContainer = null)
        {
            if (parentContainer == this)
                throw new ArgumentException($"{nameof(parentContainer)} cannot be a parent of itself");

            _parentContainer = parentContainer;
            _resolver = new DependenciesResolver(storage);
        }
        

        public object Resolve(Type dependencyType, string dependencyTag = null)
        {
            var dependencyInstance = _resolver.Resolve(dependencyType, dependencyTag);

            if (dependencyInstance is not null)
                return dependencyInstance;

            return _parentContainer is not null
                ? _parentContainer.Resolve(dependencyType, dependencyTag)
                : throw new DependencyMissingException(dependencyType, dependencyTag);
        }

        public TDependencyType Resolve<TDependencyType>(string dependencyTag = null) => (TDependencyType)Resolve(typeof(TDependencyType), dependencyTag);
    }
}