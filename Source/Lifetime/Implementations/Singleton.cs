
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NocInjector
{
    internal sealed class Singleton : LifetimeImplementation
    {
        private object _dependencyInstance;
        public Singleton(IDependency linkedDependency, DependencyInjector injector) : base(linkedDependency, injector)
        {
            _dependencyInstance = linkedDependency.Instance ?? GetSingleton();
        }

        public override object GetInstance()
        {
            return _dependencyInstance ?? GetSingleton();
        }

        private object GetSingleton()
        {
            var dependencyType = LinkedDependency.DependencyType;

            if (dependencyType.IsSubclassOf(typeof(MonoBehaviour)))
            {
                var dependencyObject = LinkedDependency.DependencyObject;

                if (dependencyObject is null)
                    _dependencyInstance = Object.FindAnyObjectByType(dependencyType) ?? throw new InvalidOperationException($"{dependencyType.Name} it does not exist on any object on the scene");
                else
                    _dependencyInstance = dependencyObject.GetComponent(dependencyType) ?? throw new InvalidOperationException($"{dependencyType.Name} it does not exist on {dependencyObject.name}");
                
                Injector.InjectInstance(_dependencyInstance);
            }
            else
            {
                _dependencyInstance = Injector.CreateInstance(dependencyType);
            }

            return _dependencyInstance;

        }
    }
}