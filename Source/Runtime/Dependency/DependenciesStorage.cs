using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NocInjector
{
    internal sealed class DependenciesStorage : IDependenciesStorage
    {
        private readonly CacheStorage _cacheStorage = new();
        private readonly ConcurrentDictionary<IDependency, LifetimeImplementation> _lifetimeContainer;

        private bool _disposed;

        public void Add(IDependency dependency, LifetimeImplementation lifetime)
        {
            if (!_lifetimeContainer.TryAdd(dependency, lifetime))
                throw new DependencyExistException(dependency);
            
            _cacheStorage.CacheDependency(dependency);
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
                return;

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