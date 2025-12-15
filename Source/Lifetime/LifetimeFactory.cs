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
        private readonly Dictionary<DependencyLifetime, Func<IDependency, DependencyInjector, LifetimeImplementation>> _implementations = new()
        {
            [DependencyLifetime.Singleton] = (dependency, injector) => new Singleton(dependency, injector),
            [DependencyLifetime.Transient] = (dependency, injector) => new Transient(dependency, injector)
        };

        private readonly DependencyInjector _dependencyInjector;

        public LifetimeFactory(DependencyInjector dependencyInjector)
        {
            _dependencyInjector = dependencyInjector;
        }
        
        
        public LifetimeImplementation GetImplementation(DependencyLifetime lifetime, IDependency dependency)
        {
            return _implementations.TryGetValue(lifetime, out var implementation)
                ? implementation(dependency, _dependencyInjector)
                : throw new UnidentifiedLifetimeException(lifetime);
        }
    }
}
