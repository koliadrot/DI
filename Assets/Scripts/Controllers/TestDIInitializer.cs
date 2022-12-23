namespace Test
{
    using UnityDev.DI;
    using UnityEngine;

    /// <summary>
    /// Тестовый инициализатор префаба через DIContanier
    /// </summary>
    public class TestDIInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab = default;

        private IFactory factoryService = default;

        [Inject]
        private void Construct(ITimerService service, IFactory factory)
        {
            factoryService = factoryService ?? factory;
            Debug.Log($"[INJECT] {nameof(IFactory)} is {factoryService != null}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                TestInstiniate();
            }
        }

        /// <summary>
        /// Создает объект через контейнер зависимостей
        /// </summary>
        public void TestInstiniate() => factoryService.InstantiateObject(prefab);
    }
}
