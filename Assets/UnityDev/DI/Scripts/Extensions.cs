namespace UnityDev.DI
{
    using UnityEngine;

    /// <summary>
    /// Расширения внедрения зависимостей
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Создает префаб с инжекцией
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static Object DIInstantiate(Object prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            Object instanceObject = Object.Instantiate(prefab, position, rotation, parent);
            DIContanier.Instance.InjectDependencies();
            return instanceObject;
        }
    }
}