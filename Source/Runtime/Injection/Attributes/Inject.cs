using System;

namespace NocInjector
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
    public sealed class Inject : Attribute
    {
        public int InjectionOrder { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="injectionOrder">Sets the priority for injection into this method</param>
        public Inject(int injectionOrder = 0)
        {
            InjectionOrder = injectionOrder;
        }
    }
}
