using System;

namespace NocInjector
{
    internal sealed class ContainerResolver
    {
        private readonly ResolverFactory _resolverFactory = new();
        
        private readonly object _resolveLock = new();
        
        public object Resolve(Type dependencyType, string dependencyTag, IDependenciesStorage dependenciesSource)
        {
            lock (_resolveLock)
            {
                var resolveMethod = _resolverFactory.SelectResolver(dependencyType);

                return resolveMethod.Resolve(dependencyType, dependencyTag, dependenciesSource);
            }
        }
    }
}