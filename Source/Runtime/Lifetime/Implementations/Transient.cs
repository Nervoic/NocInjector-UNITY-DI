
using UnityEngine;
using Object = UnityEngine.Object;

namespace NocInjector
{
    public sealed class Transient : LifetimeImplementation
    {
        
        internal Transient(IDependency dependency, IInstanceHandler instanceHandler) : base(dependency, instanceHandler)
        {
            
        }

        public override object GetInstance()
        {
            var dependencyType = Dependency.DependencyType;
            object dependencyInstance;
            
            if (dependencyType.IsSubclassOf(typeof(MonoBehaviour)))
            {
                var dependencyObject = Dependency.DependencyObject;

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