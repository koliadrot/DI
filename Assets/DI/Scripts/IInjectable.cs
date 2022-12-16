namespace UnityDev.DI
{
    /// <summary>
    /// Интерфейс метки для инжекции
    /// </summary>
    public interface IInjectable
    {
        /// <summary>
        /// Инжект зависимостей
        /// </summary>
        void Inject();
    }
}