namespace Test
{
    using System;
    using UnityDev.DI;
    using UnityEngine;

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
            checkpointService = checkpointService ?? Service<ICheckpointService>.Get();
            Debug.Log($"[INJECT] {nameof(ICheckpointService)} is {checkpointService != null}");
        }
    }
}
