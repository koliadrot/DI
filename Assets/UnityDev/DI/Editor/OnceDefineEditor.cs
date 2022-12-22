namespace UnityDev.DI
{
    using System.IO;
    using UnityEditor;

    /// <summary>
    /// Одноразовый файл для уставноки дефайна
    /// </summary>
    public class OnceDefineEditor
    {
        [InitializeOnLoadMethod]
        public static void CheckInitDefine() => DefineCheckerEditor.AddDefine(CheckDeleteFile);

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

        private static string GetPath() => Path.Combine("Assets", nameof(UnityDev), nameof(DI), "Editor", $"{nameof(OnceDefineEditor)}.cs");
    }
}
