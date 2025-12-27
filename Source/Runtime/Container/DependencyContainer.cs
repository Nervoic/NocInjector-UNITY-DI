using System;

namespace NocInjector
{
    internal sealed class DependencyContainer : IDependencyContainer
    {
        public IDependencyContainer Parent { get; }

        private readonly DependenciesStorage _storage;
        private readonly DependenciesResolver _resolver;

        private bool _disposed;
        
        
        internal DependencyContainer(IDependencyContainer parentContainer, DependenciesStorage storage, DependenciesResolver resolver)
        {
            if (parentContainer is not null)
            {
                if (parentContainer == this)
                    throw new ContainerInheritanceException($"{nameof(parentContainer)} cannot be a parent of itself");

                while (parentContainer.Parent is not null)
                {
                    if (parentContainer.Parent == this)
                        throw new ContainerInheritanceException("Cyclic inheritance in containers");
                }
            }

            Parent = parentContainer;

            _storage = storage;
            _resolver = resolver;
        }


        public object Resolve(Type dependencyType, string dependencyTag = null)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependencyContainer));
            
            var dependencyInstance = _resolver.Resolve(dependencyType, dependencyTag);

            if (dependencyInstance is not null)
                return dependencyInstance;

            return Parent is not null
                ? Parent.Resolve(dependencyType, dependencyTag)
                : throw new DependencyMissingException(dependencyType, dependencyTag);
        }

        public TDependencyType Resolve<TDependencyType>(string dependencyTag = null) => (TDependencyType)Resolve(typeof(TDependencyType), dependencyTag);

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            
            _storage.Dispose();
        }
    }
}