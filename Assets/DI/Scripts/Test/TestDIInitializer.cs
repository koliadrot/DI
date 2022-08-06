using DIService;
using UnityEngine;

namespace Test
{
    public class TestDIInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Extensions.DIInstantiate(prefab);
            }
        }
    }
}
