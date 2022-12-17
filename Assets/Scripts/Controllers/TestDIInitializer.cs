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
        public void TestInstiniate() => Extensions.DIInstantiate(prefab);
    }
}
