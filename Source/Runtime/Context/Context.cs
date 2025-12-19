
using System;
using System.Collections.Generic;

namespace NocInjector
{
    /// <summary>
    /// Context that is independent of the game cycle
    /// </summary>
    public class Context : IContext, IDisposable
    {
        private bool _disposed;
        
        /// <summary>
        /// Context-bound container
        /// </summary>
        public IDependencyContainer Container { get; private set; }
        
        /// <summary>
        /// Context-sensitive injector
        /// </summary>
        public IDependencyInjector Injector { get; private set; }

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
        
        /// <summary>
        /// Creates a new context that is independent of the game cycle
        /// </summary>
        /// <param name="parentContext"></param>
        /// <param name="installers">Dependencies installers for this context</param>
        public Context(IContext parentContext, List<IInstaller> installers) => Construct(installers.ToArray(), parentContext);
        
        private void Construct(IInstaller[] installers, IContext parentContext = null)
        {
            var constructor = new ContainerConstructor();

            foreach (var installer in installers)
                installer.Install(constructor);

            var constructionResult = constructor.Construct(parentContext?.Container);

            Injector = constructionResult.Item1;
            Container = constructionResult.Item2;
        }

        public void Dispose()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Context));

            _disposed = true;
            Container.Dispose();
        }
    }
}