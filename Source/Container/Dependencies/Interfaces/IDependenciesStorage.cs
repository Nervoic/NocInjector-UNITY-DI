using System;
using System.Collections.Generic;

namespace NocInjector
{
    internal interface IDependenciesStorage
    {
        public IDependency GetDependency(Type dependencyType, string dependencyTag);

        public IEnumerable<IDependency> GetDependencies(Type dependencyType);

        public bool TryGetDependency(Type dependencyType, string dependencyTag, out IDependency dependency);

        public bool TryGetDependencies(Type dependencyType, out IEnumerable<IDependency> dependencies);

        public LifetimeImplementation GetLifetime(IDependency dependency);
    }
}