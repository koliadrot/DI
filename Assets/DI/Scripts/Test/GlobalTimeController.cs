using DIService;
using System;
using UnityEngine;

namespace Test
{
    public class GlobalTimeController : MonoBehaviour, ITimerService, IInjectable
    {
        public event Action onTimed = delegate { };

        private ICheckpointService checkpointService;

        public void TimeIsUp()
        {
            Debug.Log("Time is UP!");
            onTimed();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                checkpointService.Checkpoint();
            }
        }

        void IInjectable.Inject()
        {
            if (checkpointService != null) return;
            checkpointService = Service<ICheckpointService>.Get();
            Debug.Log($"[INJECT] {nameof(ICheckpointService)} is {checkpointService != null}");
        }
    }
}
