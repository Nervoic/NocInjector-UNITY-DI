
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NocInjector
{
    /// <summary>
    /// A game wrapper for dividing containers into contexts
    /// </summary>
    public abstract class MonoContext : MonoBehaviour, IContext
    {
        /// <summary>
        /// The context from which the dependencies will be inherited
        /// </summary>
        [SerializeField] private MonoContext parentContext;
        
        /// <summary>
        /// Dependencies installers for this context
        /// </summary>
        [SerializeField] private List<MonoInstaller> installers = new();
        
        /// <summary>
        /// Context-bound container
        /// </summary>
        public IDependencyContainer Container { get; private set; }

        protected void Awake() => Initialize();
        private void Initialize()
        {
            var constructor = new ContainerConstructor();
            
            foreach (var installer in installers.Where(i => i is not null))
                installer.Install(constructor);
            
            Container = constructor.Construct(parentContext?.Container);
            
            OnInitialize();
        }
        
        /// <summary>
        /// Called after the context initialization is complete
        /// </summary>
        protected virtual void OnInitialize() { }
    }
}