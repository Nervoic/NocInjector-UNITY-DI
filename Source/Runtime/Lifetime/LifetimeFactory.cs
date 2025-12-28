using System;
using System.Collections.Generic;

namespace NocInjector
{
    /// <summary>
    /// It selects the implementation of lifetime for registered dependencies
    /// </summary>
    internal sealed class LifetimeFactory
    {
        /// <summary>
        /// Description of all Lifetime implementations
        /// </summary>
        private readonly Dictionary<DependencyLifetime, Func<IDependency, IInstanceHandler, LifetimeImplementation>> _implementations = new()
        {
            [DependencyLifetime.Singleton] = (dependency, instanceHandler) => new Singleton(dependency, instanceHandler),
            [DependencyLifetime.Transient] = (dependency, instanceHandler) => new Transient(dependency, instanceHandler),
        };
        
        public LifetimeImplementation Create(IDependency dependency, IInstanceHandler instanceHandler, DependencyLifetime lifetime)
        {
            return _implementations.TryGetValue(lifetime, out var implementation)
                ? implementation(dependency, instanceHandler)
                : throw new UnidentifiedLifetimeException(lifetime);
        }
    }
}
