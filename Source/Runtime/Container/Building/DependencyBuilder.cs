
using System.Collections.Generic;
using NocInjector.Exceptions;
using UnityEngine;

namespace NocInjector
{
    internal sealed class DependencyBuilder
    {
        private readonly BuilderValidator _validator = new();
        
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
                _validator.Validate(registeredDependency);

                registeredDependency.LifetimeImplementation = lifetimeFactory.Create(registeredDependency, injector, lifetime);
            }

            return injector;
        }

        private sealed class BuilderValidator
        {
            public void Validate(IDependency registeredDependency)
            {
                var registeredType = registeredDependency.DependencyType;
            
                if (registeredType.IsSubclassOf(typeof(MonoBehaviour)) && registeredDependency.DependencyObject == null)
                    throw new DependencyBuildException($"You cannot register the {registeredType} component without setting its GameObject");
            }
        }
    }
}