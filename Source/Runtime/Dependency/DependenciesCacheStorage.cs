using System;
using System.Collections.Generic;
using System.Linq;

namespace NocInjector
{ 
    internal sealed class DependenciesCacheStorage : IDisposable
    {
        private readonly Dictionary<Type, List<IDependency>> _typeCache = new();
        private readonly Dictionary<(Type, string), IDependency> _infoCache = new();

        public void CacheDependency(IDependency dependency)
        {
            var dependencyType = dependency.DependencyType;
            var abstractionType = dependency.AbstractionType;

            var dependencyTag = dependency.DependencyTag;

            AddCache(dependencyType, dependencyTag, dependency);

            if (abstractionType is not null)
                AddCache(abstractionType, dependencyTag, dependency);
        }
        
        private void AddCache(Type typeToCache, string dependencyTag, IDependency dependency)
        {
            _typeCache.TryAdd(typeToCache, new List<IDependency>());
            _typeCache[typeToCache].Add(dependency);
            
            if (!_infoCache.TryAdd((typeToCache, dependencyTag), dependency))
                throw new RepeatedDependencyException(typeToCache, dependencyTag);
        }

        public void InvalidateCache(IDependency dependency)
        {
            var dependencyType = dependency.DependencyType;
            var abstractionType = dependency.AbstractionType;

            var dependencyTag = dependency.DependencyTag;
            
            RemoveCache(dependencyType, dependencyTag, dependency);
            
            if (abstractionType is not null)
                RemoveCache(abstractionType, dependencyTag, dependency);
        }
        
        private void RemoveCache(Type cachedType, string dependencyTag, IDependency dependency)
        {
            if (!_typeCache.TryGetValue(cachedType, out var dependencies))
                throw new DependencyMissingException(cachedType);
            
            dependencies.Remove(dependency);

            if (dependencies.Count == 0)
                _typeCache.Remove(cachedType);

            if (!_infoCache.Remove((cachedType, dependencyTag)))
                throw new DependencyMissingException(cachedType, dependencyTag);
        }

        public IDependency GetDependencyFromCache(Type dependencyType, string dependencyTag)
        {
            return _infoCache.GetValueOrDefault((dependencyType, dependencyTag));
        }

        public IEnumerable<IDependency> GetDependenciesFromCache(Type dependencyType) => _typeCache.GetValueOrDefault(dependencyType);

        public void Dispose()
        {
            
        }
    }
}