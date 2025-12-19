using System;
using System.Collections.Generic;

namespace NocInjector
{
    internal sealed class DependenciesStorage : IDependenciesStorage
    {
        private readonly DependenciesCacheStorage _cacheStorage = new();
        private readonly Dictionary<IDependency, LifetimeImplementation> _lifetimeContainer = new();

        private bool _disposed;

        public void Add(IDependency dependency, LifetimeImplementation lifetime)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));
            
            if (!_lifetimeContainer.TryAdd(dependency, lifetime))
                throw new DependencyExistException(dependency);
            
            _cacheStorage.CacheDependency(dependency);
        }

        public void Remove(IDependency dependency)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));
            
            if (!_lifetimeContainer.Remove(dependency))
                throw new DependencyUnexistException(dependency);
            
            _cacheStorage.InvalidateCache(dependency);
        }

        public IDependency GetDependency(Type dependencyType, string dependencyTag)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));
            
            var dependency = _cacheStorage.GetDependencyFromCache(dependencyType, dependencyTag);

            return dependency ?? throw new DependencyMissingException(dependencyType, dependencyTag);
        }

        public IEnumerable<IDependency> GetDependencies(Type dependencyType)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));
            
            var dependencies = _cacheStorage.GetDependenciesFromCache(dependencyType);

            return dependencies ?? throw new DependencyMissingException(dependencyType);
        }

        public bool TryGetDependency(Type dependencyType, string dependencyTag, out IDependency dependency)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));
            
            dependency = _cacheStorage.GetDependencyFromCache(dependencyType, dependencyTag);

            return dependency is not null;
        }

        public bool TryGetDependencies(Type dependencyType, out IEnumerable<IDependency> dependencies)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));
            
            dependencies = _cacheStorage.GetDependenciesFromCache(dependencyType);

            return dependencies is not null;
        }

        public LifetimeImplementation GetLifetime(IDependency dependency) => _lifetimeContainer.GetValueOrDefault(dependency);

        public void Dispose()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));

            _disposed = true;

            foreach (var lifetime in _lifetimeContainer.Values)
            {
                if (lifetime is not IDisposable disposable)
                    continue;
                
                disposable.Dispose();
            }
        }
    }
}