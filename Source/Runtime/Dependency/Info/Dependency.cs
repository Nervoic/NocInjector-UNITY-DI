using System;
using UnityEngine;

namespace NocInjector
{
    /// <summary>
    /// Dependency information
    /// </summary>
    internal class Dependency : IDependency
    {
        public Type DependencyType { get; }
        public Type AbstractionType { get; }

        public LifetimeImplementation LifetimeImplementation { get; set; }
        public GameObject DependencyObject { get; set; }

        public string DependencyTag { get; set; }
        public object Instance { get; set; }

        public Dependency(Type dependencyType, Type abstractionType = null)
        {
            DependencyType = dependencyType;
            AbstractionType = abstractionType;
        }
    }
}