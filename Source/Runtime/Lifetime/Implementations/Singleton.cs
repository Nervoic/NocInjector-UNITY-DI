
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NocInjector
{
    internal sealed class Singleton : LifetimeImplementation, IDisposable
    {
        private object _dependencyInstance;
        private bool _disposed;
        public Singleton(IDependency linkedDependency, IInstanceHandler instanceHandler) : base(linkedDependency, instanceHandler)
        {
            _dependencyInstance = linkedDependency.Instance ?? GetSingleton();
        }

        public override object GetInstance()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Singleton));
            
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
                
                InstanceHandler.InjectInstance(_dependencyInstance);
            }
            else
            {
                _dependencyInstance = InstanceHandler.CreateInstance(dependencyType);
            }

            return _dependencyInstance;
        }

        public void Dispose()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Singleton));

            _disposed = true;
            
            if (_dependencyInstance is IDisposable disposable)
                disposable.Dispose();
        }
    }
}