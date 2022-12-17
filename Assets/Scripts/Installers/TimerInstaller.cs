namespace Test
{
    using UnityDev.DI;
    using UnityEngine;

    /// <summary>
    /// Инсталлер таймера
    /// </summary>
    public class TimerInstaller : AbstractInjectionInstaller
    {
        [SerializeField]
        private GlobalTimeController timerController = default;

        public override void InstallBindings(DIContanier dIContanier) => dIContanier.Bind(timerController);
    }
}
