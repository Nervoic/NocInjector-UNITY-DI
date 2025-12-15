using System;

namespace NocInjector
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class Tag : Attribute
    {
        public string InjectionTag { get; }

        public Tag(string injectionTag)
        {
            InjectionTag = injectionTag;
        }
    }
}