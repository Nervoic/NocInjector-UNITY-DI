using System;

namespace NocInjector.Exceptions
{
    internal sealed class DependencyBuildException : Exception
    {
        public DependencyBuildException(string message) : base(message) { }
    }
}