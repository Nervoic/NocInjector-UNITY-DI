using System;

namespace NocInjector
{
    internal interface IResolver
    {
        public bool SupportType(Type dependencyType);
        public object Resolve(Type dependencyType, string dependencyTag, IDependenciesStorage dependenciesSource);
    }
}