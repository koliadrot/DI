namespace Test
{
    using System;

    /// <summary>
    /// Сервис глобального таймера
    /// </summary>
    public interface ITimerService
    {
        /// <summary>
        /// Событие о конце работе таймера
        /// </summary>
        event Action OnTimed;

        /// <summary>
        /// Завершить таймер
        /// </summary>
        void TimeIsUp();
    }
}
