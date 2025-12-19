
using System;

namespace NocInjector
{
    internal abstract class LifetimeImplementation

    {
        /// <summary>
        /// Dependency belonging to this lifetime
        /// </summary>
        protected IDependency LinkedDependency { get; }
        
        protected IInstanceHandler InstanceHandler { get; }

        protected LifetimeImplementation(IDependency linkedDependency, IInstanceHandler instanceHandler)
        {
            LinkedDependency = linkedDependency;
            InstanceHandler = instanceHandler;
        }

        /// <summary>
        /// Gets an instance of the dependency
        /// </summary>
        /// <returns></returns>
        public abstract object GetInstance();
    }
}