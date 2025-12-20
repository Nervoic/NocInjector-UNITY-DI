using System;

namespace NocInjector
{
    internal sealed class DependencyContainer : IDependencyContainer
    {
        private readonly IDependencyContainer _parentContainer;
        private readonly IDependenciesStorage _dependenciesStorage;
        
        private readonly ContainerResolver _resolver = new();

        private bool _disposed = false;
        
        
        internal DependencyContainer(IDependenciesStorage storage, IDependencyContainer parentContainer = null)
        {
            if (parentContainer == this)
                throw new ArgumentException($"{nameof(parentContainer)} cannot be a parent of itself");

            _parentContainer = parentContainer;
            _dependenciesStorage = storage;
        }


        public object Resolve(Type dependencyType, string dependencyTag = null)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependencyContainer));
            
            var dependencyInstance = _resolver.Resolve(dependencyType, dependencyTag, _dependenciesStorage);

            if (dependencyInstance is not null)
                return dependencyInstance;

            return _parentContainer is not null
                ? _parentContainer.Resolve(dependencyType, dependencyTag)
                : throw new DependencyMissingException(dependencyType, dependencyTag);
        }

        public TDependencyType Resolve<TDependencyType>(string dependencyTag = null) => (TDependencyType)Resolve(typeof(TDependencyType), dependencyTag);

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            
            _dependenciesStorage.Dispose();
        }
    }
}