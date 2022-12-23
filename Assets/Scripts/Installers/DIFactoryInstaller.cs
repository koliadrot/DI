namespace Test
{
    using UnityDev.DI;

    /// <summary>
    /// Тестовый инсталлер фабрики
    /// </summary>
    public class DIFactoryInstaller : AbstractInjectionInstaller
    {
        private DIFactory dIFactory = default;

        public override void InstallBindings(DIContanier dIContanier)
        {
            dIFactory = dIFactory ?? new DIFactory();
            dIContanier.Bind(dIFactory);
        }
    }
}
