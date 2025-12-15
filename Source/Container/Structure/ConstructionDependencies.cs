using System.Collections.Generic;

namespace NocInjector
{
    internal sealed class ConstructionDependencies
    {
        private readonly Dictionary<Dependency, DependencyLifetime> _constructionDependencies = new();
        

        public void Add(Dependency dependency, DependencyLifetime lifetime) => _constructionDependencies.Add(dependency, lifetime);

        public void Remove(Dependency dependency) => _constructionDependencies.Remove(dependency);
        

        public IReadOnlyDictionary<Dependency, DependencyLifetime> GetDictionary() => _constructionDependencies;
    }
}