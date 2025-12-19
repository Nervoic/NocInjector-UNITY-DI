
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NocInjector
{
    internal sealed class Transient : LifetimeImplementation
    {
        
        public Transient(IDependency linkedDependency, IInstanceHandler instanceHandler) : base(linkedDependency, instanceHandler)
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
                
                dependencyInstance = createdObject.GetComponent(dependencyType);
                InstanceHandler.InjectObject(createdObject);
            }
            else
            {
                dependencyInstance = InstanceHandler.CreateInstance(dependencyType);
            }
            
            return dependencyInstance;
        }
    }
}