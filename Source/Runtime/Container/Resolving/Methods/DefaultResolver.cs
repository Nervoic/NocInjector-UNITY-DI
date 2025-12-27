using System;

namespace NocInjector
{
    /// <summary>
    /// Resolves standard dependencies
    /// </summary>
    internal class DefaultResolver : IResolver
    {
        public bool SupportType(Type dependencyType) => !dependencyType.IsArray;

        public object Resolve(Type dependencyType, string dependencyTag, IDependenciesStorage dependenciesSource)
        {
            if (!dependenciesSource.TryGetDependency(dependencyType, dependencyTag, out var dependency))
                return null;

            var lifetimeImplementation = dependency.LifetimeImplementation;

            return lifetimeImplementation.GetInstance();
        }
    }
}