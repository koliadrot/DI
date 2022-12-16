using System;

namespace Test
{
    public interface ICheckpointService
    {
        event Action onCheckpointed;
        void Checkpoint();
    }
}
