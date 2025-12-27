using System;

namespace NocInjector
{
    internal sealed class DependencyExistException : Exception
    {
        public DependencyExistException(IDependency dependency) : base($"{dependency?.DependencyType} already registered in the container")
        {
            
        }
    }
}