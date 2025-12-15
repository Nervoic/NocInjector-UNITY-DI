using System;

namespace NocInjector
{
    internal sealed class DependenciesResolver
    {
        private readonly IDependenciesStorage _dependenciesStorage;

        private readonly ResolveMethodFactory _resolveMethodFactory = new();
        
        private readonly object _resolveLock = new();

        public DependenciesResolver(IDependenciesStorage dependenciesStorage)
        {
            _dependenciesStorage = dependenciesStorage;
        }
        public object Resolve(Type dependencyType, string dependencyTag)
        {
            lock (_resolveLock)
            {
                var resolveMethod = _resolveMethodFactory.Select(dependencyType);

                return resolveMethod.Resolve(dependencyType, dependencyTag, _dependenciesStorage);
                
            }
        }
    }
}