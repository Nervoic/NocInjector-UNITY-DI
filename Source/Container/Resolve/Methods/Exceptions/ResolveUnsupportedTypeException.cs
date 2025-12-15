using System;

namespace NocInjector
{
    public class ResolveUnsupportedTypeException : Exception
    {
        public ResolveUnsupportedTypeException(Type dependencyType) : base($"{dependencyType.Name} type resolve is not supported") { }
    }
}