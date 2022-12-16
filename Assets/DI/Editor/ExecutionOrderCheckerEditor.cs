namespace UnityDev.DI
{
    using System;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Проверяет перед запуском у ключевых скриптов
    /// порядок инициа-ции перед стартом.
    /// </summary>
    public class ExecutionOrderCheckerEditor
    {
        /// <summary>
        /// Автовалидация порядка вызовов
        /// </summary>
        [MenuItem(nameof(UnityDev) + "/" + nameof(DI) + "/" + nameof(Validate))]
        public static void Validate()
        {
            int startOrder = -10000;

            SetExecutionOrder(typeof(DIContanier), ref startOrder);
        }

        /// <summary>
        /// Автовалидация порядка вызовов с ивентом
        /// </summary>
        /// <param name="action"></param>
        public static void Validate(Action action)
        {
            Validate();
            action?.Invoke();
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
