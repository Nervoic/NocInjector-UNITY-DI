using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NocInjector
{ 
    internal sealed class CacheStorage
    {
        private readonly ConcurrentDictionary<Type, List<IDependency>> _typeCache = new();
        private readonly ConcurrentDictionary<(Type, string), IDependency> _infoCache = new();

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

        public IDependency GetDependencyFromCache(Type dependencyType, string dependencyTag)
        {
            return _infoCache.GetValueOrDefault((dependencyType, dependencyTag));
        }

        public IEnumerable<IDependency> GetDependenciesFromCache(Type dependencyType) => _typeCache.GetValueOrDefault(dependencyType);
    }
}