
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace NocInjector
{
    /// <summary>
    /// A game wrapper for dividing containers into contexts
    /// </summary>
    public class MonoContext : MonoBehaviour, IContext
    {
        /// <summary>
        /// The context from which the dependencies will be inherited
        /// </summary>
        [SerializeField] private MonoContext parentContext;

        /// <summary>
        /// Dependencies installers for this context
        /// </summary>
        [SerializeField] private MonoInstaller[] installers;

        [SerializeField] private GameObject[] injectionObjects;
        
        /// <summary>
        /// Context-bound container
        /// </summary>
        public IDependencyContainer Container { get; private set; }
        
        /// <summary>
        /// Context-sensitive injector
        /// </summary>
        public IDependencyInjector Injector { get; private set; }

        protected void Awake() => Initialize();
        private void Initialize()
        {
            var constructor = new ContainerConstructor();
            
            foreach (var installer in installers.Where(i => i is not null))
                installer.Install(constructor);

            var constructionResult = constructor.Construct(parentContext?.Container);

            Injector = constructionResult.Item1;
            Container = constructionResult.Item2;
            
            InjectObjects();
            OnInitialize();
        }

        private void InjectObjects()
        {
            foreach (var injectionObject in injectionObjects)
                Injector.InjectObject(injectionObject);
        }
        
        
        
        /// <summary>
        /// Creates a new child context that is independent of the unity
        /// </summary>
        /// <param name="subContextInstallers"></param>
        /// <returns></returns>
        public Context GetSubContext(params IInstaller[] subContextInstallers) => new Context(this, subContextInstallers);
        
        /// <summary>
        /// Creates a new child context that is independent of the unity
        /// </summary>
        /// <param name="subContextInstallers"></param>
        /// <returns></returns>
        
        public Context GetSubContext(List<IInstaller> subContextInstallers) => new Context(this, subContextInstallers);
        
        /// <summary>
        /// Called after the context initialization is complete
        /// </summary>
        protected virtual void OnInitialize() { }

        private void OnDestroy()
        {
            Container.Dispose();
        }
    }
}