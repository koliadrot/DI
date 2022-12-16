namespace UnityDev.DI
{
    /// <summary>
    /// Сервис по регистрации/получении зависимостей
    /// </summary>
    public static class Service<T> where T : class
    {
        private static T instance;

        /// <summary>
        /// Возвращает зависимость
        /// </summary>
        /// <returns></returns>
        public static T Get() => instance;

        /// <summary>
        /// Регистрирует зависимость
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T Set(T instance)
        {
            Service<T>.instance = instance;
            return Service<T>.instance;
        }
    }
}