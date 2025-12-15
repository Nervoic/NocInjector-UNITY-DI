using System;

namespace NocInjector
{
    internal interface IResolveMethod
    {
        public abstract bool SupportResolveType(Type dependencyType);

        public abstract object Resolve(Type dependencyType, string dependencyTag, IDependenciesStorage dependenciesStorage);
    }
}