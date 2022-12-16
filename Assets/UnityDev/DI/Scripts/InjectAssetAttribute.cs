namespace UnityDev.DI
{
    using System;

    /// <summary>
    /// Инжекция зависимостей
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAssetAttribute : Attribute
    {
        /// <summary>
        /// Маркер инжекции
        /// </summary>
        public InjectAssetAttribute() { }
    }
}
