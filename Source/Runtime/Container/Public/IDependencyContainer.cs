using System;

namespace NocInjector
{
    public interface IDependencyContainer : IDisposable
    {
        public IDependencyContainer Parent { get; }
        public object Resolve(Type dependencyType, string dependencyTag = null);
        public TDependencyType Resolve<TDependencyType>(string dependencyTag = null);
    }
}