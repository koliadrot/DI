namespace Test
{
    using UnityDev.DI;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Вьюшка текста контрольной точки
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class CheckpointView : MonoBehaviour
    {
        private ICheckpointService checkpointService = default;
        private Text text = default;

        [Inject]
        private void Construct(ICheckpointService service)
        {
            if (checkpointService == null)
            {
                text = GetComponent<Text>();
                checkpointService = service;
                Debug.Log($"[INJECT] VIEW {nameof(ICheckpointService)} is {checkpointService != null}");
                if (checkpointService != null)
                {
                    checkpointService.OnCheckpointed += UpdateText;
                }
            }
        }

        private void OnDestroy()
        {
            if (checkpointService != null)
            {
                checkpointService.OnCheckpointed -= UpdateText;
            }
        }

        private void UpdateText() => text.text = "Update!!!";
    }
}
