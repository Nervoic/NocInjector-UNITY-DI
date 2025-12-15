using System;
using System.Linq;

namespace NocInjector
{
    /// <summary>
    /// Resolves arrays
    /// </summary>
    internal class ArrayResolver : IResolveMethod
    {
        public bool SupportResolveType(Type dependencyType) => dependencyType.IsArray;
        public object Resolve(Type arrayType, string dependencyTag, IDependenciesStorage dependenciesStorage)
        {
            var dependencyType = arrayType.GetElementType();

            if (dependencyType is null)
                return null;

            if (!dependenciesStorage.TryGetDependencies(dependencyType, out var dependencies))
                return null;

            if (dependencyTag is not null)
                dependencies = dependencies.Where(dependency => dependency.DependencyTag == dependencyTag);

            var dependenciesArray = dependencies.ToArray();
            var dependenciesInstances = new object[dependenciesArray.Length];
            
            for (var i = 0; i < dependenciesInstances.Length; i++)
            {
                var currentDependency = dependenciesArray[i];
                var lifetimeImplementation = dependenciesStorage.GetLifetime(currentDependency);

                dependenciesInstances[i] = lifetimeImplementation.GetInstance();
            }

            var instancesArray = Array.CreateInstance(dependencyType, dependenciesInstances.Length);
            Array.Copy(dependenciesInstances, instancesArray, dependenciesInstances.Length);

            return instancesArray;
        }
    }
}