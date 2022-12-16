namespace Test
{
    using UnityDev.DI;
    using UnityEngine;

    public class CheckpointInstaller : AbstractInjectionInstaller
    {
        [SerializeField]
        private CheckpointController checkpointController;

        public override void Bind() => Service<ICheckpointService>.Set(checkpointController);

        public override void Clear() => Service<ICheckpointService>.Set(null);
    }
}