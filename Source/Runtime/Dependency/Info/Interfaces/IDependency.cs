using System;
using UnityEngine;

namespace NocInjector
{
    public interface IDependency
    {
        public Type DependencyType { get; }
        public Type AbstractionType { get; }

        public string DependencyTag { get; }
        public object Instance { get; }
        public GameObject DependencyObject { get; }
    }
}