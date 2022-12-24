namespace Test
{
    using UnityDev.DI;
    using UnityEngine;

    /// <summary>
    /// Инсталлер контроллера
    /// </summary>
    public class NumberInstaller : AbstractInjectionInstaller
    {
        [SerializeField]
        private NumberController[] numberControllers = default;

        public override void InstallBindings(DIContanier dIContanier) => dIContanier.Bind(numberControllers);
    }
}
