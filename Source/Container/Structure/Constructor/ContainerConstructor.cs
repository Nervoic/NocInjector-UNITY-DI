
namespace NocInjector
{
    /// <summary>
    /// Registers dependencies in the container.
    /// </summary>
    internal sealed class ContainerConstructor : IContainerConstructor
    {
        private readonly ConstructorValidator _validator = new();

        private readonly ConstructionDependencies _constructionDependencies = new();        
        private readonly object _constructLock = new();
        
        
        /// <summary>
        /// Creates a new dependency registration branch.
        /// </summary>
        /// <param name="lifetime">The lifetime for the dependency being registered</param>
        /// <typeparam name="TDependencyType">The specific type of the dependency being registered</typeparam>
        /// <returns></returns>
        public ConstructorRegistration<TDependencyType> Register<TDependencyType>(DependencyLifetime lifetime) 
            where TDependencyType : class
        {
            lock (_constructLock)
            {
                var dependencyType = typeof(TDependencyType);
                
                _validator.ValidateRegistration(dependencyType);
                
                var constructionDependency = new Dependency(dependencyType);
                _constructionDependencies.Add(constructionDependency, lifetime);

                return new ConstructorRegistration<TDependencyType>(_constructionDependencies, constructionDependency);
            }
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
            lock (_constructLock)
            {
                var dependencyType = typeof(TDependencyType);
                var abstractionType = typeof(TAbstractionType);
                
                _validator.ValidateRegistration(dependencyType, abstractionType);

                var constructionDependency = new Dependency(dependencyType, abstractionType);
                _constructionDependencies.Add(constructionDependency, lifetime);

                return new ConstructorRegistration<TAbstractionType>(_constructionDependencies, constructionDependency);
            }
        }

        public IDependencyContainer Construct(IDependencyContainer parentContainer = null)
        {
            lock (_constructLock)
            {
                var storage = new DependenciesStorage();
                var container = new DependencyContainer(storage, parentContainer);

                ConstructLifetimes(container, storage);

                return container;
            }
        }

        private void ConstructLifetimes(IDependencyContainer container, DependenciesStorage storage)
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
        }
        
    }
}