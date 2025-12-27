using System;

namespace NocInjector
{
    internal sealed class DependencyUnexistException : Exception
    {
        public DependencyUnexistException(IDependency dependency) : base($"{dependency?.DependencyType} not registered in the container and cannot be deleted")
        {
            
        }
    }
}