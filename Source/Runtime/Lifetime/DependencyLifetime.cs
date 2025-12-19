using System;

namespace NocInjector
{
    /// <summary>
    /// Specifies the lifetime of a registered service.
    /// </summary>
    
    [Flags]
    public enum DependencyLifetime
    {
        Singleton = 0,
        Transient = 1
    }
}
