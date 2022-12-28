#if DI
namespace UnityDev.DI
{
    using System;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Устанавливает порядок иниц-ии у
    /// ключевых сприптов
    /// </summary>
    public class ExecutionOrderCheckerEditor
    {
        private const string KEY_SAVE = "Execution Order";

        /// <summary>
        /// Автовалидация порядка вызовов
        /// </summary>
        [MenuItem(nameof(UnityDev) + "/" + nameof(DI) + "/" + nameof(Validate))]
        public static void Validate()
        {
            int startOrder = -10000;

            SetExecutionOrder(typeof(DIContanier), ref startOrder);
        }

        [InitializeOnLoadMethod]
        private static void OnceValidate()
        {
            if (!PlayerPrefs.HasKey(KEY_SAVE))
            {
                Validate();
                PlayerPrefs.SetString(KEY_SAVE, KEY_SAVE);
                PlayerPrefs.Save();
            }
        }

        private static void SetExecutionOrder(Type type, ref int order)
        {
            MonoScript objectExecution = MonoImporter.GetAllRuntimeMonoScripts().Where(x => x.name == type.Name).FirstOrDefault();

            if (objectExecution != null)
            {
                MonoImporter.SetExecutionOrder(objectExecution, order);
                order++;
            }
            else
            {
                Debug.LogError($"Не найден MonoScript {type.Name} для ExecutionOrder");
            }
        }
    }
}
#endif