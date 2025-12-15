
using UnityEngine;
using Object = UnityEngine.Object;

namespace NocInjector
{
    internal sealed class Transient : LifetimeImplementation
    {
        public Transient(IDependency linkedDependency, DependencyInjector injector) : base(linkedDependency, injector)
        {
            
        }

        public override object GetInstance()
        {
            var dependencyType = LinkedDependency.DependencyType;
            object dependencyInstance;
            
            if (dependencyType.IsSubclassOf(typeof(MonoBehaviour)))
            {
                var dependencyObject = LinkedDependency.DependencyObject;

                var createdObject = Object.Instantiate(dependencyObject);
                
                Injector.InjectObject(createdObject);
                dependencyInstance = createdObject.GetComponent(dependencyType);
            }
            else
            {
                dependencyInstance = Injector.CreateInstance(dependencyType);
            }
            
            return dependencyInstance;
        }
        
        
    }
}