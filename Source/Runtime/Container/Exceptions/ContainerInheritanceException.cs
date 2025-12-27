using System;

namespace NocInjector
{
    public class ContainerInheritanceException : Exception
    {
        public ContainerInheritanceException(string message) : base(message) { }
    }
}