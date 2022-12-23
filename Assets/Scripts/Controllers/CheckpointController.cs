namespace Test
{
    using System;
    using UnityDev.DI;
    using UnityEngine;

    /// <summary>
    /// Чекпоинт контроллер
    /// </summary>
    public class CheckpointController : MonoBehaviour, ICheckpointService
    {
        public event Action OnCheckpointed = delegate { };

        private ITimerService timerService;

        [Inject]
        private void Construct(ITimerService service)
        {
            timerService = timerService ?? service;
            Debug.Log($"[INJECT] {nameof(ITimerService)} is {timerService != null}");
        }

        void ICheckpointService.OnCheckpoint()
        {
            Debug.Log("Checkpoint");
            OnCheckpointed();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckpointTest();
            }
        }

        /// <summary>
        /// Тестовый вызов завершения рабты таймера
        /// </summary>
        public void CheckpointTest() => timerService.TimeIsUp();
    }
}
