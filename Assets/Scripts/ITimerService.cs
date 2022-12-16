using System;

namespace Test
{
    public interface ITimerService
    {
        event Action onTimed;
        void TimeIsUp();
    }
}
