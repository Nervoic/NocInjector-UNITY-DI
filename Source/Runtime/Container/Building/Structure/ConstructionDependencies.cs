using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NocInjector
{
    internal sealed class ConstructionDependencies
    {
        private readonly ConcurrentDictionary<Dependency, DependencyLifetime> _constructionDependencies = new();
        
        public void Add(Dependency dependency, DependencyLifetime lifetime) => _constructionDependencies.TryAdd(dependency, lifetime);

        public void Remove(Dependency dependency) => _constructionDependencies.TryRemove(dependency, out _);
        

        public IReadOnlyDictionary<Dependency, DependencyLifetime> GetDictionary() => _constructionDependencies;
    }
}