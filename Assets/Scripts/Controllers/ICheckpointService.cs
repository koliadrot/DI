namespace Test
{
    using System;

    /// <summary>
    /// Сервис контрольной точки
    /// </summary>
    public interface ICheckpointService
    {
        /// <summary>
        /// Событие о дистигнутой контрольной точке
        /// </summary>
        event Action OnCheckpointed;

        /// <summary>
        /// Вызов контрольной точки
        /// </summary>
        void OnCheckpoint();
    }
}
