using System;
using UnityEngine;

namespace NocInjector
{
    internal interface IDependency
    {
        public Type DependencyType { get; }
        public Type AbstractionType { get; }
        
        public GameObject DependencyObject { get; }
        public LifetimeImplementation LifetimeImplementation { get; }

        public string DependencyTag { get; }
        public object Instance { get; }
    }
}