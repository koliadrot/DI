using DIService;
using System;
using UnityEngine;

namespace Test
{
    public class CheckpointController : MonoBehaviour, ICheckpointService, IInjectable
    {
        public event Action onCheckpointed = delegate { };

        private ITimerService timerService;

        public void Checkpoint()
        {
            Debug.Log("Checkpoint");
            onCheckpointed();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                timerService.TimeIsUp();
            }
        }

        void IInjectable.Inject()
        {
            if (timerService != null) return;
            timerService = Service<ITimerService>.Get();
            Debug.Log($"[INJECT] {nameof(ITimerService)} is {timerService != null}");
        }
    }
}
