using DIService;
using UnityEngine;

namespace Test
{
    public class TimerInstaller : AbstractInjectionInstaller
    {
        [SerializeField]
        private GlobalTimeController timerController;

        public override void Bind()
        {
            Service<ITimerService>.Set(timerController);
        }

        public override void Clear()
        {
            Service<ICheckpointService>.Set(null);
        }
    }
}
