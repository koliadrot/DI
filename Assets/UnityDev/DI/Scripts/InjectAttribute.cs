#if DI
namespace UnityDev.DI
{
    using System;

    /// <summary>
    /// Инжекция зависимостей
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {
        /// <summary>
        /// Маркер инжекции
        /// </summary>
        public InjectAttribute() { }
    }
}
#endif