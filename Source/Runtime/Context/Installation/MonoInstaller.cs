
using UnityEngine;

namespace NocInjector
{
    /// <summary>
    /// Installer that depends on the game cycle
    /// </summary>
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        public abstract void Install(IContainerBuilder builder);
    }
}
