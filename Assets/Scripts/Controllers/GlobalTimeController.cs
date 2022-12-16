namespace Test
{
    using System;
    using UnityDev.DI;
    using UnityEngine;

    /// <summary>
    /// Контроллер глобального времени
    /// </summary>
    public class GlobalTimeController : MonoBehaviour, ITimerService
    {
        public event Action OnTimed = delegate { };

        private ICheckpointService checkpointService;

        [InjectAsset]
        private void Construct(ICheckpointService service)
        {
            checkpointService = checkpointService ?? service;
            Debug.Log($"[INJECT] {nameof(ICheckpointService)} is {checkpointService != null}");
        }

        void ITimerService.TimeIsUp()
        {
            Debug.Log("Time is UP!");
            OnTimed();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                checkpointService.OnCheckpoint();
            }
        }

        /// <summary>
        /// Тестовый вызов достижения контрольной точки
        /// </summary>
        public void TimeIsUpTest() => checkpointService.OnCheckpoint();
    }
}
