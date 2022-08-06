using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DIService
{
    /// <summary>
    /// Контейнер зависимостей
    /// </summary>
    //TODO:Изменить инжекцию с помощью рефлексии атрибутов
    public class DIContanier : MonoBehaviour
    {
        [SerializeField]
        private List<AbstractInjectionInstaller> installers;
        private GameObject[] components;

        private void Awake() => Init();
        private void OnDestroy() => Clear();

        private void Init()
        {
            foreach (AbstractInjectionInstaller installer in installers)
            {
                installer.Bind();
            }
            Inject();
        }
        private void Clear()
        {
            foreach (AbstractInjectionInstaller installer in installers)
            {
                installer.Clear();
            }
        }

        /// <summary>
        /// Внедряет зависимости
        /// </summary>
        public void Inject()
        {
            components = Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.gameObject.scene.IsValid()).ToArray();
            foreach (GameObject component in components)
            {
                if (component.TryGetComponent(out IInjectable injectable))
                {
                    injectable.Inject();
                }
            }
        }

    }
}
