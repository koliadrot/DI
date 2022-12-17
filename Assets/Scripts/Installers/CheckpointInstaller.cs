namespace Test
{
    using UnityDev.DI;
    using UnityEngine;

    /// <summary>
    /// Инсталлер контрольной точки
    /// </summary>
    public class CheckpointInstaller : AbstractInjectionInstaller
    {
        [SerializeField]
        private CheckpointController checkpointController = default;

        public override void InstallBindings(DIContanier dIContanier) => dIContanier.Bind(checkpointController);
    }
}