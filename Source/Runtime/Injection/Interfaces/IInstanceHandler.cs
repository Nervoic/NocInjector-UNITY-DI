using System;

namespace NocInjector
{
    internal interface IInstanceHandler : IDependencyInjector
    {
        public object CreateInstance(Type instanceType);
    }
}