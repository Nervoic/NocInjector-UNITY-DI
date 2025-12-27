
using System;
using System.Collections.Generic;
using NocInjector.Exceptions;
using UnityEngine;

namespace NocInjector
{
    /// <summary>
    /// Registers dependencies in the container.
    /// </summary>
    internal sealed class ContainerBuilder : IContainerBuilder
    {
        private readonly BuilderValidator _validator = new();

        private readonly Dictionary<Dependency, DependencyLifetime> _registeredDependencies;

        private bool _isBuilt;



        /// <summary>
        /// Creates a new dependency registration branch.
        /// </summary>
        /// <param name="lifetime">The lifetime for the dependency being registered</param>
        /// <typeparam name="TDependencyType">The specific type of the dependency being registered</typeparam>
        /// <returns></returns>
        public DependencyRegistration<TDependencyType> Register<TDependencyType>(DependencyLifetime lifetime)
            where TDependencyType : class
        {
            if (_isBuilt)
                throw new ContainerBuiltException();
            
            var dependencyType = typeof(TDependencyType);

            _validator.ValidateRegistration(dependencyType);

            var registeredDependency = new Dependency(dependencyType);
            _registeredDependencies.Add(registeredDependency, lifetime);

            return new DependencyRegistration<TDependencyType>(registeredDependency);
        }


        /// <summary>
        /// Creates a new dependency registration branch.
        /// </summary>
        /// <param name="lifetime">The lifetime for the dependency being registered</param>
        /// <typeparam name="TAbstractionType">The type of abstraction that can be used to obtain the dependency</typeparam>
        /// <typeparam name="TDependencyType">The specific type of dependency being registered</typeparam>
        /// <returns></returns>
        public DependencyRegistration<TAbstractionType> Register<TAbstractionType, TDependencyType>(DependencyLifetime lifetime)
            where TAbstractionType : class
            where TDependencyType : class, TAbstractionType
        {
            if (_isBuilt)
                throw new ContainerBuiltException();

            var dependencyType = typeof(TDependencyType);
            var abstractionType = typeof(TAbstractionType);

            _validator.ValidateRegistration(dependencyType, abstractionType);

            var constructionDependency = new Dependency(dependencyType, abstractionType);
            _registeredDependencies.Add(constructionDependency, lifetime);

            return new DependencyRegistration<TAbstractionType>(constructionDependency);
        }

        public (IDependencyInjector, IDependencyContainer) Build(IDependencyContainer parentContainer = null)
        {
            if (_isBuilt)
                throw new ContainerBuiltException();

            var storage = new DependenciesStorage(_registeredDependencies.Keys);
            var resolver = new DependenciesResolver(storage);
            
            var container = new DependencyContainer(parentContainer, storage, resolver);
            var injector = InitializeLifetimes(container);

            _isBuilt = true;
            return (injector, container);
        }

        private IDependencyInjector InitializeLifetimes(IDependencyContainer container)
        {
            var injector = new DependencyInjector(container);
            var lifetimeFactory = new LifetimeFactory();
            
            foreach (var (registeredDependency, lifetime) in _registeredDependencies)
            {
                _validator.ValidateBuild(registeredDependency);

                registeredDependency.LifetimeImplementation = lifetimeFactory.CreateImplementation(registeredDependency, injector, lifetime);
            }

            return injector;
        }
        
        private sealed class BuilderValidator
        {
            private readonly HashSet<Type> _validatedRegistrationTypes = new();
            public void ValidateRegistration(Type dependencyType, Type abstractionType = null)
            {
                if (_validatedRegistrationTypes.Contains(dependencyType) || _validatedRegistrationTypes.Contains(abstractionType))
                    return;
               
                ValidateType(dependencyType, abstractionType);
            }

            private void ValidateType(Type dependencyType, Type abstractionType)
            {
                if (dependencyType.IsAbstract)
                    throw new ArgumentException($"Cannot register an abstract dependency {dependencyType.Name} without specifying an implementation");

                _validatedRegistrationTypes.Add(dependencyType);

                if (abstractionType is not null)
                {
                    if (!abstractionType.IsAbstract)
                        throw new ArgumentException($"Cannot register {abstractionType.Name} as an abstraction of {dependencyType.Name} because it is not abstract");

                    _validatedRegistrationTypes.Add(abstractionType);
                }
            }

            public void ValidateBuild(IDependency constructionDependency)
            {
                var constructionDependencyType = constructionDependency.DependencyType;
            
                if (constructionDependencyType.IsSubclassOf(typeof(MonoBehaviour)) && constructionDependency.DependencyObject == null)
                    throw new InvalidOperationException($"You cannot register the {constructionDependency.DependencyType.Name} component without setting its GameObject");
            
            }
        }
        
    }
}