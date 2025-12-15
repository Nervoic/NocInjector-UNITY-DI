
using System.Collections.Generic;

namespace NocInjector
{
    /// <summary>
    /// Context that is independent of the game cycle
    /// </summary>
    public class Context : IContext
    {
        /// <summary>
        /// Context-bound container
        /// </summary>
        public IDependencyContainer Container { get; private set; }
        
        /// <summary>
        /// Creates a new context that is independent of the game cycle
        /// </summary>
        /// <param name="parentContext"></param>
        /// <param name="installers">Dependencies installers for this context</param>

        public Context(IContext parentContext = null, params IInstaller[] installers) => Construct(installers, parentContext);
        
        /// <summary>
        /// Creates a new context that is independent of the game cycle
        /// </summary>
        /// <param name="installers">Dependencies installers for this context</param>
        
        public Context(params IInstaller[] installers) => Construct(installers);
        
        /// <summary>
        /// Creates a new context that is independent of the game cycle
        /// </summary>
        /// <param name="installers">Dependencies installers for this context</param>
        
        public Context(List<IInstaller> installers) => Construct(installers.ToArray());
        
        private void Construct(IInstaller[] installers, IContext parentContext = null)
        {
            var constructor = new ContainerConstructor();

            foreach (var installer in installers)
                installer.Install(constructor);

            Container = constructor.Construct(parentContext?.Container);
        }
    }
}