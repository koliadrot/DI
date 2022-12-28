#if DI
namespace UnityDev.DI
{
    using UnityEngine;

    /// <summary>
    /// Инсталлер зависимости
    /// </summary>
    public abstract class AbstractInjectionInstaller : MonoBehaviour
    {
        /// <summary>
        /// Устанавливает зависимость
        /// </summary>
        public abstract void InstallBindings(DIContanier dIContanier);
    }
}
#endif