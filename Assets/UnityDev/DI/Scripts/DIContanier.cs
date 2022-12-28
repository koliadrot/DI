#if DI
namespace UnityDev.DI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Object = UnityEngine.Object;

    /// <summary>
    /// Контейнер зависимостей
    /// </summary>
    ///NOTE:Многие foreach и Linq конструкции были заменены в угоду скорости при работе
    ///с большим кол-вом данных.
    public class DIContanier : MonoBehaviour
    {
        public static DIContanier Instance = null;

        [SerializeField]
        private List<AbstractInjectionInstaller> installers = new List<AbstractInjectionInstaller>();

        private Dictionary<Type, object> poolInstallers = new Dictionary<Type, object>();

        private List<MonoBehaviour> injectedObjectects = new List<MonoBehaviour>();
        private List<MonoBehaviour> components = new List<MonoBehaviour>();

        private List<FastMethodInfo> methods = new List<FastMethodInfo>();
        private MethodInfo[] methodInfos = default;
        private MethodInfo methodInfo = default;


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

            FullObjectsInject();
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
        public T InstantiateObject<T>(T prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Component
        {
            T instanceObject = Instantiate(prefab, position, rotation, parent);
            InjectToObject(instanceObject);
            return instanceObject.GetComponent<T>();
        }

        public T InstantiateObject<T>(T prefab, Transform parent) where T : Component => InstantiateObject(prefab, default, default, parent);

        /// <summary>
        /// Внедряет зависимости всем объектам на сцене
        /// </summary>
        public void FullObjectsInject()
        {
            methods.Clear();
            components.Clear();
            CollectSceneObjects(components);
            InjectDependencies(components);
        }

        /// <summary>
        /// Внедряет зависимости у конкретного объекта
        /// </summary>
        /// <param name="component"></param>
        public void InjectToObject(Component component)
        {
            methods.Clear();
            components.Clear();
            GetInjectableMonoBehavioursUnderGameObjectInternal(component.gameObject, components);
            InjectDependencies(components);
        }

        private void CollectSceneObjects(List<MonoBehaviour> injectableMonoBehaviours)
        {
            Scene scene = gameObject.scene;
            foreach (var rootObj in GetRootGameObjects(scene))
            {
                GetInjectableMonoBehavioursUnderGameObjectInternal(rootObj, injectableMonoBehaviours);
            }
        }

        private void InjectDependencies(List<MonoBehaviour> injectableMonoBehaviours)
        {
            for (int i = 0; i < injectableMonoBehaviours.Count; i++)
            {
                if (injectedObjectects.Count > 1)
                {
                    bool isCanCheck = true;
                    for (int k = 0; k < injectedObjectects.Count; k++)
                    {
                        if (injectableMonoBehaviours[i] == injectedObjectects[k])
                        {
                            isCanCheck = false;
                            break;
                        }
                    }
                    if (isCanCheck)
                    {
                        AddMethod(InjectToMethods(injectableMonoBehaviours[i]));
                    }
                }
                else
                {
                    AddMethod(InjectToMethods(injectableMonoBehaviours[i]));
                }
            }

            for (int i = 0; i < methods.Count; i++)
            {
                methods[i].Invoke();
            }
        }

        private FastMethodInfo InjectToMethods(Object obj)
        {
            methodInfo = default;
            methodInfos = obj.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < methodInfos.Length; i++)
            {
                if (methodInfos[i].GetCustomAttribute<InjectAttribute>() is InjectAttribute)
                {
                    methodInfo = methodInfos[i];
                    break;
                }
            }

            if (methodInfo != null)
            {
                object[] actualVatiables = new object[methodInfo.GetParameters().Length];
                for (int i = 0; i < methodInfo.GetParameters().Length; i++)
                {
                    foreach (Type installerKey in poolInstallers.Keys)
                    {
                        if (methodInfo.GetParameters()[i].ParameterType.IsAssignableFrom(installerKey) && poolInstallers.TryGetValue(installerKey, out object installer))
                        {
                            actualVatiables[i] = installer;
                        }
                    }
                }

                FastMethodInfo method = new FastMethodInfo(methodInfo, obj, actualVatiables);

                if (actualVatiables.Length == methodInfo.GetParameters().Length)
                {
                    injectedObjectects.Add(obj as MonoBehaviour);
                }
                return method;
            }
            return null;
        }

        private void AddMethod(FastMethodInfo fastMethodInfo)
        {
            if (fastMethodInfo != null && !methods.Contains(fastMethodInfo))
            {
                methods.Add(fastMethodInfo);
            }
        }

        public IEnumerable<GameObject> GetRootGameObjects(Scene scene)
        {
            if (scene.isLoaded)
            {
                return scene.GetRootGameObjects();
            }
            else
            {
                return Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.transform.parent == null && x.scene == scene);
            }
        }

        private void GetInjectableMonoBehavioursUnderGameObjectInternal(GameObject rootObject, List<MonoBehaviour> injectableComponents)
        {
            if (rootObject != null)
            {
                MonoBehaviour[] monoBehaviours = rootObject.GetComponents<MonoBehaviour>();

                for (int i = 0; i < rootObject.transform.childCount; i++)
                {
                    var child = rootObject.transform.GetChild(i);

                    if (child != null)
                    {
                        GetInjectableMonoBehavioursUnderGameObjectInternal(child.gameObject, injectableComponents);
                    }
                }

                for (int i = 0; i < monoBehaviours.Length; i++)
                {
                    if (monoBehaviours[i] != null)
                    {
                        injectableComponents.Add(monoBehaviours[i]);
                    }
                }
            }
        }
    }
}
#endif
