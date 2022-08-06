using System.Linq;
using UnityEngine;

namespace DIService
{
    /// <summary>
    /// Расширения
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Находит объект среди всех на сцене
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindObject<T>() where T : class
        {
            Component[] components = Resources.FindObjectsOfTypeAll<Component>().Where(x => x.gameObject.scene.IsValid()).ToArray();
            ScriptableObject[] so = Resources.FindObjectsOfTypeAll<ScriptableObject>();
            return components.FirstOrDefault(x => x.GetType() == typeof(T)) as T ?? so.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
        }

        /// <summary>
        /// Создает префаб с инжекцией
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static Object DIInstantiate(Object prefab)
        {
            DIContanier contanier = FindObject<DIContanier>();
#if UNITY_EDITOR
            Debug.LogError($"[DI] is {contanier != null}");
#endif
            if (contanier == null) return null;
            Object instanceObject = Object.Instantiate(prefab);
            contanier.Inject();
            return instanceObject;
        }
    }
}