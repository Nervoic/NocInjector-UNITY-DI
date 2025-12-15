
namespace NocInjector
{
    /// <summary>
    /// Installers base interface
    /// </summary>
    public interface IInstaller
    {
        /// <summary>
        /// Sets dependencies in the container being assembled
        /// </summary>
        /// <param name="constructor">Container constructor</param>
        public void Install(IContainerConstructor constructor);
    }
}