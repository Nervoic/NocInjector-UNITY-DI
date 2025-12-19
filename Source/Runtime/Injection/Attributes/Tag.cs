using System;

namespace NocInjector
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class Tag : Attribute
    {
        public string InjectionTag { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="injectionTag">A dependency tag that must be implemented</param>
        public Tag(string injectionTag)
        {
            InjectionTag = injectionTag;
        }
    }
}