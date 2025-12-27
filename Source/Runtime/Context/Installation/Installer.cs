

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
        /// <param name="builder">Container constructor</param>
        public abstract void Install(IContainerBuilder builder);
    }
}