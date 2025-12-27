using System;
using UnityEngine;

namespace NocInjector
{
    /// <summary>
    /// Floating registration for construction dependencies
    /// </summary>
    /// <typeparam name="TRegisteredType"></typeparam>
    public sealed class DependencyRegistration<TRegisteredType>
    {
        private readonly Dependency _registeredDependency;
        internal DependencyRegistration(Dependency registeredDependency)
        {
            _registeredDependency = registeredDependency;
        }

        /// <summary>
        /// Adds a Tag to the registered dependency.
        /// </summary>
        /// <param name="tag">Tag for the dependency.</param>
        public DependencyRegistration<TRegisteredType> WithTag(string tag)
        {
            _registeredDependency.DependencyTag = tag;

            return this;
        }

        /// <summary>
        /// Set instance to this dependency
        /// </summary>
        /// <param name="instance">Dependency instance</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DependencyRegistration<TRegisteredType> WithInstance(TRegisteredType instance)
        {
            if (instance is null) 
                throw new ArgumentException($"Cannot set an instance for {_registeredDependency.DependencyType.Name} as null");
            
            _registeredDependency.Instance = instance;

            return this; 
        }
        
        /// <summary>
        /// Set GameObject for component
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public DependencyRegistration<TRegisteredType> OnGameObject(GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentNullException(nameof(gameObject));
            
            if (!_registeredDependency.DependencyType.IsSubclassOf(typeof(MonoBehaviour)))
                throw new InvalidOperationException($"{_registeredDependency.DependencyType.Name} is not component, and it can't have an object.");
            
            _registeredDependency.DependencyObject = gameObject;

            return this;
        }
    }
}