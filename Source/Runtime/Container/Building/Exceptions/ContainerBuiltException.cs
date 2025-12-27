using System;

namespace NocInjector.Exceptions
{
    public class ContainerBuiltException : Exception
    {
        public ContainerBuiltException() : base("The container is already built, and you cannot continue to register dependencies in it") { }
    }
}