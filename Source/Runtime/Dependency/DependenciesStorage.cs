using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NocInjector
{ 
    internal sealed class DependenciesStorage : IDependenciesStorage
    {
        private readonly ConcurrentDictionary<Type, List<IDependency>> _typeStorage = new();
        private readonly ConcurrentDictionary<(Type, string), IDependency> _infoStorage = new();

        private bool _disposed = false;

        public DependenciesStorage(IEnumerable<IDependency> dependencies)
        {
            foreach (var dependency in dependencies)
                SaveDependency(dependency);
        }

        private void SaveDependency(IDependency dependency)
        {
            var dependencyType = dependency.DependencyType;
            var abstractionType = dependency.AbstractionType;

            var dependencyTag = dependency.DependencyTag;

            AddDependency(dependencyType, dependencyTag, dependency);

            if (abstractionType is not null)
                AddDependency(abstractionType, dependencyTag, dependency);
        }
        
        private void AddDependency(Type typeToCache, string dependencyTag, IDependency dependency)
        {
            _typeStorage.TryAdd(typeToCache, new List<IDependency>());
            _typeStorage[typeToCache].Add(dependency);

            if (!_infoStorage.TryAdd((typeToCache, dependencyTag), dependency))
                throw new RepeatedDependencyException(typeToCache, dependencyTag);
        }

        public IDependency GetDependency(Type dependencyType, string dependencyTag)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));
            
            return _infoStorage.TryGetValue((dependencyType, dependencyTag), out var dependency)
                ? dependency
                : throw new DependencyMissingException(dependencyType, dependencyTag);
        }

        public IEnumerable<IDependency> GetDependencies(Type dependencyType)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));
            
            return _typeStorage.TryGetValue(dependencyType, out var dependencies)
                ? dependencies
                : throw new DependencyMissingException(dependencyType);
        }

        public bool TryGetDependency(Type dependencyType, string dependencyTag, out IDependency dependency)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));

            return _infoStorage.TryGetValue((dependencyType, dependencyTag), out dependency);
        }

        public bool TryGetDependencies(Type dependencyType, out IEnumerable<IDependency> dependencies)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependenciesStorage));
            
            var successful = _typeStorage.TryGetValue(dependencyType, out var dependenciesList);
            dependencies = dependenciesList;
            
            return successful;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
 
            foreach (var dependency in _infoStorage.Values)
            {
                if (dependency.LifetimeImplementation is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}