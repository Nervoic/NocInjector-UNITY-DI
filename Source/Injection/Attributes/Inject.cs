using System;

namespace NocInjector
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
    public sealed class Inject : Attribute
    {
        
    }
}
