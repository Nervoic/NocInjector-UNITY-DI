
using System;

namespace NocInjector
{
    /// <summary>
    /// Registers dependencies in the container.
    /// </summary>
    internal sealed class ContainerConstructor : IContainerConstructor
    {
        private readonly ConstructorValidator _validator = new();

        private readonly ConstructionDependencies _constructionDependencies = new();

        private bool _isConstructed = false;



        /// <summary>
        /// Creates a new dependency registration branch.
        /// </summary>
        /// <param name="lifetime">The lifetime for the dependency being registered</param>
        /// <typeparam name="TDependencyType">The specific type of the dependency being registered</typeparam>
        /// <returns></returns>
        public ConstructorRegistration<TDependencyType> Register<TDependencyType>(DependencyLifetime lifetime)
            where TDependencyType : class
        {
            if (_isConstructed)
                throw new InvalidOperationException($"You cannot start registration after building the container");
            var dependencyType = typeof(TDependencyType);

            _validator.ValidateRegistration(dependencyType);

            var constructionDependency = new Dependency(dependencyType);
            _constructionDependencies.Add(constructionDependency, lifetime);

            return new ConstructorRegistration<TDependencyType>(_constructionDependencies, constructionDependency);
        }


        /// <summary>
        /// Creates a new dependency registration branch.
        /// </summary>
        /// <param name="lifetime">The lifetime for the dependency being registered</param>
        /// <typeparam name="TAbstractionType">The type of abstraction that can be used to obtain the dependency</typeparam>
        /// <typeparam name="TDependencyType">The specific type of dependency being registered</typeparam>
        /// <returns></returns>
        public ConstructorRegistration<TAbstractionType> Register<TAbstractionType, TDependencyType>(DependencyLifetime lifetime)
            where TAbstractionType : class
            where TDependencyType : class, TAbstractionType
        {
            if (_isConstructed)
                throw new InvalidOperationException($"You cannot start registration after building the container");

            var dependencyType = typeof(TDependencyType);
            var abstractionType = typeof(TAbstractionType);

            _validator.ValidateRegistration(dependencyType, abstractionType);

            var constructionDependency = new Dependency(dependencyType, abstractionType);
            _constructionDependencies.Add(constructionDependency, lifetime);

            return new ConstructorRegistration<TAbstractionType>(_constructionDependencies, constructionDependency);
        }

        public (IDependencyInjector, IDependencyContainer) Construct(IDependencyContainer parentContainer = null)
        {
            if (_isConstructed)
                throw new InvalidOperationException($"You cannot start construction after building the container");

            var storage = new DependenciesStorage();
            var container = new DependencyContainer(storage, parentContainer);

            var injector = ConstructLifetimes(container, storage);

            _isConstructed = true;
            return (injector, container);
        }

        private IDependencyInjector ConstructLifetimes(IDependencyContainer container, DependenciesStorage storage)
        {
            var injector = new DependencyInjector(container);
            var lifetimeFactory = new LifetimeFactory(injector);

            var dependenciesDictionary = _constructionDependencies.GetDictionary();
            foreach (var (constructionDependency, lifetime) in dependenciesDictionary)
            {
                _validator.ValidateConstruct(constructionDependency);

                var lifetimeImplementation = lifetimeFactory.GetImplementation(lifetime, constructionDependency);
                storage.Add(constructionDependency, lifetimeImplementation);
            }

            return injector;
        }
        
    }
}