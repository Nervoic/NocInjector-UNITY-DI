
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace NocInjector
{
    internal sealed class DependencyInjector : IInstanceHandler
    {
        private readonly IDependencyContainer _linkedContainer;
        public DependencyInjector(IDependencyContainer linkedContainer)
        {
            _linkedContainer = linkedContainer;
        }
        public object CreateInstance(Type dependencyType)
        {
            var injectConstructor = dependencyType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(constructor => constructor.IsDefined(typeof(Inject)));

            if (injectConstructor is null)
            {
                var createdInstance = InjectInstance(Activator.CreateInstance(dependencyType));

                return createdInstance;
            }

            var acceptedDependencies = injectConstructor.GetParameters();
            var dependenciesInstances = GetInjectedParameters(acceptedDependencies);

            var injectedInstance = injectConstructor.Invoke(dependenciesInstances);
            
            return injectedInstance;
        }

        public object InjectInstance(object injectableInstance)
        {
            var injectableMethods = injectableInstance.GetType()
                .GetMethods()
                .Where(method => method.IsDefined(typeof(Inject)))
                .OrderBy(method => method.GetCustomAttribute<Inject>().InjectionOrder);

            foreach (var injectableMethod in injectableMethods)
            {
                var acceptedDependencies = injectableMethod.GetParameters();

                var dependenciesInstances = GetInjectedParameters(acceptedDependencies);

                injectableMethod.Invoke(injectableInstance, dependenciesInstances);
            }

            return injectableInstance;
        }

        public void InjectObject(GameObject injectableObject)
        {
            foreach (var component in injectableObject.GetComponents<MonoBehaviour>())
                InjectInstance(component);
        }
        
        private object[] GetInjectedParameters(ParameterInfo[] acceptedDependencies)
        {
            var dependenciesInstances = new object[acceptedDependencies.Length];

            for (var i = 0; i < acceptedDependencies.Length; i++)
            {
                var currentDependency = acceptedDependencies[i];
                var currentDependencyType = currentDependency.ParameterType;

                if (!currentDependency.IsDefined(typeof(Tag)))
                {
                    dependenciesInstances[i] = _linkedContainer.Resolve(currentDependencyType);
                    continue;
                }
                
                var tagAttr = currentDependency.GetCustomAttribute<Tag>();
                var injectionTag = tagAttr.InjectionTag;

                dependenciesInstances[i] = _linkedContainer.Resolve(currentDependencyType, injectionTag);
            }

            return dependenciesInstances;
        }
    }
}