namespace UnityDev.DI
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Editor скрипт для определения текущей версии плагина.
    /// </summary>
    public class GetVersionPluginEditor
    {
        private const string CURRENT_VERSION = "1.1.3";

        [MenuItem("UnityDev/DI/Version - " + CURRENT_VERSION)]
        public static void GetVersionInfo() => Debug.Log("DI версия - " + CURRENT_VERSION);
    }
}
