namespace UnityDev.DI
{
    using System.IO;
    using UnityEditor;

    /// <summary>
    /// Одноразовый файл для установки ExecutionOrder
    /// </summary>
    public class OnceExecutionOrderEditor
    {
        [InitializeOnLoadMethod]
        public static void CheckInitExecutionOrder() => ExecutionOrderCheckerEditor.Validate(CheckDeleteFile);

        /// <summary>
        /// Проверяет файл
        /// </summary>
        private static void CheckDeleteFile()
        {
            if (File.Exists(GetPath()))
            {
                File.Delete(GetPath());
            }
        }

        private static string GetPath() => Path.Combine("Assets", nameof(UnityDev), nameof(DI), "Editor", $"{nameof(OnceExecutionOrderEditor)}.cs");
    }
}
