using System;

namespace NocInjector
{
    /// <summary>
    /// Resolves standard dependencies
    /// </summary>
    internal class DefaultResolver : IResolveMethod
    {
        public bool SupportResolveType(Type dependencyType) => !dependencyType.IsArray;

        public object Resolve(Type dependencyType, string dependencyTag, IDependenciesStorage dependenciesStorage)
        {
            if (!dependenciesStorage.TryGetDependency(dependencyType, dependencyTag, out var dependency))
                return null;
            
            var lifetimeImplementation = dependenciesStorage.GetLifetime(dependency);

            return lifetimeImplementation.GetInstance();
        }
    }
}