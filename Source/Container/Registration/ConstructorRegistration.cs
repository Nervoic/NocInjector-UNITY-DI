using System;
using UnityEngine;

namespace NocInjector
{
    /// <summary>
    /// Floating registration for construction dependencies
    /// </summary>
    /// <typeparam name="TRegisteredType"></typeparam>
    public sealed class ConstructorRegistration<TRegisteredType>
    {
        private readonly ConstructionDependencies _constructionDependencies;
        private readonly Dependency _dependency;
        internal ConstructorRegistration(ConstructionDependencies constructionDependencies, Dependency dependency)
        {
            _constructionDependencies = constructionDependencies;
            _dependency = dependency;
        }

        /// <summary>
        /// Adds a Tag to the registered dependency.
        /// </summary>
        /// <param name="tag">Tag for the dependency.</param>
        public ConstructorRegistration<TRegisteredType> WithTag(string tag)
        {
            _dependency.DependencyTag = tag;

            return this;
        }

        /// <summary>
        /// Set instance to this dependency
        /// </summary>
        /// <param name="instance">Dependency instance</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ConstructorRegistration<TRegisteredType> WithInstance(TRegisteredType instance)
        {
            if (instance is null) 
                throw new ArgumentException($"Cannot set an instance for {_dependency.DependencyType.Name} as null");
            
            _dependency.Instance = instance;

            return this; 
        }
        
        /// <summary>
        /// Set GameObject for component
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ConstructorRegistration<TRegisteredType> OnGameObject(GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentNullException(nameof(gameObject));
            
            if (!_dependency.DependencyType.IsSubclassOf(typeof(MonoBehaviour)))
                throw new InvalidOperationException($"{_dependency.DependencyType.Name} is not component, and it can't have an object.");
            
            _dependency.DependencyObject = gameObject;

            return this;
        }
        
        /// <summary>
        /// Cancels the installation of a dependency when Condition is false
        /// </summary>
        /// <param name="condition"></param>
        public void When(bool condition)
        {
            if (!condition)
                _constructionDependencies.Remove(_dependency);
        }
    }
}