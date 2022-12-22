namespace UnityDev.DI
{
    using System;
    using UnityEditor;

    /// <summary>
    /// Editor класс для добавления define
    /// </summary>
    public class DefineCheckerEditor
    {
        private const string DEFINE = "DI";

        /// <summary>
        /// Добавить дефайн для всех платформ
        /// </summary>
        /// <returns></returns>
        [MenuItem("UnityDev/DI/Добавить для всех платформ дефайн - " + DEFINE)]
        public static void AddDefines()
        {

            foreach (var targetName in Enum.GetNames(typeof(BuildTarget)))
            {
                var target = (BuildTarget)Enum.Parse(typeof(BuildTarget), targetName);
                var build = BuildPipeline.GetBuildTargetGroup(target);
                var definitions = PlayerSettings.GetScriptingDefineSymbolsForGroup(build);
                if (!definitions.Contains(DEFINE) && build != BuildTargetGroup.Unknown)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(build, definitions + ";" + DEFINE);
                }
            }
        }

        /// <summary>
        /// Добавить дефайн для текущей
        /// </summary>
        /// <returns></returns>
        [MenuItem("UnityDev/DI/Добавить дефайн - " + DEFINE)]
        public static void AddDefine()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            var build = BuildPipeline.GetBuildTargetGroup(target);
            var definitions = PlayerSettings.GetScriptingDefineSymbolsForGroup(build);
            if (!definitions.Contains(DEFINE))
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(build, definitions + ";" + DEFINE);
            }
        }

        /// <summary>
        /// Добавить дефайн для текущей
        /// </summary>
        /// <returns></returns>
        public static void AddDefine(Action callback)
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            var build = BuildPipeline.GetBuildTargetGroup(target);
            var definitions = PlayerSettings.GetScriptingDefineSymbolsForGroup(build);
            if (!definitions.Contains(DEFINE))
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(build, definitions + ";" + DEFINE);
                callback?.Invoke();
            }
        }
    }
}
