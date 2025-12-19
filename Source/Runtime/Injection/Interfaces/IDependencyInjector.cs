using UnityEngine;

namespace NocInjector
{
    public interface IDependencyInjector
    {
        public void InjectObject(GameObject injectableObject);
        public void InjectInstance(object injectableInstance);
    }
}