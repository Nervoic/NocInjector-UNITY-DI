using System;

namespace NocInjector
{
    public class UnidentifiedLifetimeException : Exception
    {
        public UnidentifiedLifetimeException(DependencyLifetime lifetime) : base($"Lifetime {lifetime} it has no implementation") { }
    }
}