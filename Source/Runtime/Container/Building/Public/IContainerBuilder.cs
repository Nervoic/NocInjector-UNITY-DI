namespace NocInjector
{
    public interface IContainerBuilder
    {
        /// <summary>
        /// Registers a dependency in the constructor's builder.
        /// </summary>
        /// <param name="lifetime">Dependency lifetime</param>
        /// <typeparam name="TDependencyType">Type of the dependency being registered</typeparam>
        /// <returns></returns>
        public DependencyRegistration<TDependencyType> Register<TDependencyType>(DependencyLifetime lifetime) 
            where TDependencyType : class;

        /// <summary>
        /// Registers a dependency in the constructor's builder.
        /// </summary>
        /// <param name="lifetime">Dependency lifetime</param>
        /// <typeparam name="TAbstractionType">Dependency abstraction</typeparam>
        /// <typeparam name="TDependencyType">Dependency type</typeparam>
        /// <returns></returns>
        public DependencyRegistration<TAbstractionType> Register<TAbstractionType, TDependencyType>(DependencyLifetime lifetime)
            where TAbstractionType : class
            where TDependencyType : class, TAbstractionType;

    }
}