using DIService;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    [RequireComponent(typeof(Text))]
    public class CheckpointView : MonoBehaviour, IInjectable
    {
        private ICheckpointService checkpointService;
        private Text text;

        private void Start()
        {
            text = GetComponent<Text>();
        }

        private void OnDestroy()
        {
            checkpointService.onCheckpointed -= UpdateText;
        }

        private void UpdateText()
        {
            text.text = "Update!!!";
        }

        void IInjectable.Inject()
        {
            if (checkpointService != null) return;
            checkpointService = Service<ICheckpointService>.Get();
            Debug.Log($"[INJECT] VIEW {nameof(ICheckpointService)} is {checkpointService != null}");
            checkpointService.onCheckpointed += UpdateText;
        }
    }
}
