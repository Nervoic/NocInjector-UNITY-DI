using System;

namespace NocInjector
{
    internal class DependencyExistException : Exception
    {
        public DependencyExistException(IDependency dependency) : base($"{dependency?.DependencyType} already registered in the container")
        {
            
        }
    }
}