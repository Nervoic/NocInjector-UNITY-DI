

namespace NocInjector
{
    /// <summary>
    /// Custom installer
    /// </summary>
    public abstract class Installer : IInstaller
    {
        /// <summary>
        /// Sets dependencies in the container being assembled
        /// </summary>
        /// <param name="constructor">Container constructor</param>
        public abstract void Install(IContainerConstructor constructor);
    }
}