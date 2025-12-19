using System;
using System.Linq;

namespace NocInjector
{
    /// <summary>
    /// Resolves arrays
    /// </summary>
    internal class ArrayResolver : IResolver
    {
        public bool SupportType(Type dependencyType) => dependencyType.IsArray;
        
        public object Resolve(Type arrayType, string dependencyTag, IDependenciesStorage dependenciesSource)
        {
            var dependencyType = arrayType.GetElementType();

            if (dependencyType is null)
                return null;

            if (!dependenciesSource.TryGetDependencies(dependencyType, out var dependencies))
                return null;

            if (dependencyTag is not null)
                dependencies = dependencies.Where(dependency => dependency.DependencyTag == dependencyTag);

            var dependenciesArray = dependencies.ToArray();
            var dependenciesInstances = new object[dependenciesArray.Length];
            
            for (var i = 0; i < dependenciesInstances.Length; i++)
            {
                var currentDependency = dependenciesArray[i];
                var lifetimeImplementation = dependenciesSource.GetLifetime(currentDependency);

                dependenciesInstances[i] = lifetimeImplementation.GetInstance();
            }

            var instancesArray = Array.CreateInstance(dependencyType, dependenciesInstances.Length);
            Array.Copy(dependenciesInstances, instancesArray, dependenciesInstances.Length);

            return instancesArray;
        }
    }
}