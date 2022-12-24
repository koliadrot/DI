namespace Test
{
    using System.Linq;
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
        private NumberController numberController = default;

        [Inject]
        private void Construct(IFactory factory, NumberController[] numberControllers)
        {
            numberController = numberController ?? numberControllers?.FirstOrDefault(x => x.Id == "2");
            factoryService = factoryService ?? factory;
            Debug.Log($"[INJECT] {nameof(IFactory)} is {factoryService != null}");
            Debug.Log($"[INJECT] {nameof(NumberController)} is {numberController != null} and number == {numberController.Id}");
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
