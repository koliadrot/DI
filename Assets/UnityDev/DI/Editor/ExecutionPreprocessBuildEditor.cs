#if DI
namespace UnityDev.DI
{
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;

    /// <summary>
    /// Запускает вилдацию ExecutionOrder перед билдом
    /// </summary>
    public class ExecutionPreprocessBuildEditor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;


        public void OnPreprocessBuild(BuildReport report) => ExecutionOrderCheckerEditor.Validate();
    }
}
#endif