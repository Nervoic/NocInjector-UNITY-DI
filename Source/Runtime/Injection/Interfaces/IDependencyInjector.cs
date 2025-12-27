using System;
using UnityEngine;

namespace NocInjector
{
    public interface IDependencyInjector
    {
        public object InjectInstance(object injectableInstance);
        
        public void InjectObject(GameObject injectableObject);
    }
}