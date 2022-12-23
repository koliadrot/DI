namespace UnityDev.DI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// Контейнер зависимостей
    /// </summary>
    public class DIContanier : MonoBehaviour
    {
        public static DIContanier Instance = null;

        [SerializeField]
        private List<AbstractInjectionInstaller> installers = new List<AbstractInjectionInstaller>();

        private Dictionary<Type, object> poolInstallers = new Dictionary<Type, object>();


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Init();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy() => Clear();

        private void Init()
        {
            foreach (AbstractInjectionInstaller installer in installers)
            {
                installer.InstallBindings(this);
            }
            InjectDependencies();
        }
        private void Clear()
        {
            Instance = null;
            poolInstallers.Clear();
        }

        /// <summary>
        /// Устанавливает зависимость в пул зависимостей
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Bind<T>(T objectType) where T : class
        {
            if (!poolInstallers.ContainsKey(objectType.GetType()))
            {
                poolInstallers.Add(objectType.GetType(), objectType);
            }
            else
            {
                Debug.LogError($"Такой тип {objectType.GetType()} уже присуствует");
            }
        }

        /// <summary>
        /// Создает префаб с инжекцией
        /// </summary>
        /// <param name="prefab">Префаб</param>
        /// <param name="position">Позиция</param>
        /// <param name="rotation">Вращение</param>
        /// <param name="parent">Родитель объекта</param>
        /// <returns></returns>
        public T InstantiateObject<T>(T prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object
        {
            T instanceObject = Instantiate(prefab, position, rotation, parent);
            InjectDependencies();
            return instanceObject;
        }

        /// <summary>
        /// Создает префаб с инжекцией
        /// </summary>
        /// <param name="prefab">Префаб</param>
        /// <param name="parent">Родитель объекта</param>
        public T InstantiateObject<T>(T prefab, Transform parent) where T : Object => InstantiateObject(prefab, default, default, parent);

        /// <summary>
        /// Внедряет зависимости
        /// </summary>
        public void InjectDependencies()
        {
            MonoBehaviour[] components = Resources.FindObjectsOfTypeAll<MonoBehaviour>().Where(x => x.gameObject.scene.IsValid()).ToArray();
            foreach (var component in components)
            {
                InjectToMethods(component);
            }
        }

        private void InjectToMethods(Object obj)
        {
            Type targetType = obj.GetType();
            MethodInfo[] allMethods = targetType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (MethodInfo methodInfo in allMethods)
            {
                if (methodInfo.GetCustomAttribute(typeof(InjectAttribute)) is InjectAttribute)
                {
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    object[] actualVatiables = new object[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        foreach (Type installerKey in poolInstallers.Keys)
                        {
                            if (parameters[i].ParameterType.IsAssignableFrom(installerKey) && poolInstallers.TryGetValue(installerKey, out object installer))
                            {
                                actualVatiables[i] = installer;
                            }
                        }
                    }
                    methodInfo.Invoke(obj, actualVatiables);
                }
            }
        }
    }
}
