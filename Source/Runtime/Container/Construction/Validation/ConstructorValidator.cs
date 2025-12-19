using System;
using System.Collections.Generic;
using UnityEngine;

namespace NocInjector
{
    internal sealed class ConstructorValidator
    {
        private readonly HashSet<Type> _validatedRegistrationTypes = new();
        public void ValidateRegistration(Type dependencyType, Type abstractionType = null)
        {
            if (!_validatedRegistrationTypes.Contains(dependencyType))
            {
                ValidateType(dependencyType);
                _validatedRegistrationTypes.Add(dependencyType);
            }

            if (abstractionType is not null && !_validatedRegistrationTypes.Contains(abstractionType))
            {
                ValidateAbstraction(dependencyType, abstractionType);
                _validatedRegistrationTypes.Add(abstractionType);
            }
        }

        private void ValidateType(Type dependencyType)
        {
            if (dependencyType.IsAbstract)
                throw new ArgumentException($"Cannot register an abstract dependency {dependencyType.Name} without specifying an implementation");
        }

        private void ValidateAbstraction(Type dependencyType, Type abstractionType)
        {
            if (!abstractionType.IsAbstract)
                throw new ArgumentException($"Cannot register {abstractionType.Name} as an abstraction of {dependencyType.Name} because it is not abstract");
        }

        public void ValidateConstruct(IDependency constructionDependency)
        {
            var constructionDependencyType = constructionDependency.DependencyType;
            
            if (constructionDependencyType.IsSubclassOf(typeof(MonoBehaviour)) && constructionDependency.DependencyObject == null)
                throw new InvalidOperationException($"You cannot register the {constructionDependency.DependencyType.Name} component without setting its GameObject");
            
        }
    }
}