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

        private Dictionary<Type, Object> poolInstallers = new Dictionary<Type, Object>();


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
        /// Устанавливает зависимость в пуль зависимостей
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectType"></param>
        public void Bind<T>(T objectType) where T : Object
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
                if (methodInfo.GetCustomAttribute(typeof(InjectAssetAttribute)) is InjectAssetAttribute)
                {
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    object[] actualVatiables = new object[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        foreach (Type installerKey in poolInstallers.Keys)
                        {
                            if (parameters[i].ParameterType.IsAssignableFrom(installerKey) && poolInstallers.TryGetValue(installerKey, out Object installer))
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
