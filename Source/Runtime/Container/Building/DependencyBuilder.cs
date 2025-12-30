
using System.Collections.Generic;

namespace NocInjector
{
    internal sealed class DependencyBuilder
    {
        private readonly Dictionary<Dependency, DependencyLifetime> _registeredDependencies;
        
        public DependencyBuilder(Dictionary<Dependency, DependencyLifetime> registeredDependencies)
        {
            _registeredDependencies = registeredDependencies;
        }
        
        public IDependencyInjector Build(IDependencyContainer container)
        {
            var injector = new DependencyInjector(container);
            var lifetimeFactory = new LifetimeFactory();
            
            foreach (var (registeredDependency, lifetime) in _registeredDependencies)
            {
                registeredDependency.LifetimeImplementation = lifetimeFactory.Create(registeredDependency, injector, lifetime);
            }

            return injector;
        }
    }
}