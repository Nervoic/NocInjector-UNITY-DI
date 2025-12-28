using System;

namespace NocInjector
{
    internal sealed class DependenciesResolver
    {
        private readonly ResolverFactory _resolverFactory = new();

        private readonly IDependenciesStorage _dependenciesSource;

        public DependenciesResolver(IDependenciesStorage dependenciesSource)
        {
            _dependenciesSource = dependenciesSource;
        }
        
        public object Resolve(Type dependencyType, string dependencyTag)
        {
            var resolver = _resolverFactory.SelectResolver(dependencyType);

            return resolver.Resolve(dependencyType, dependencyTag, _dependenciesSource);
        }
    }
}