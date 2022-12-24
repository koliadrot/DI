namespace Test
{

    using UnityEngine;

    /// <summary>
    /// Контроллер цифры
    /// </summary>
    public class NumberController : MonoBehaviour
    {
        [SerializeField]
        private string id = string.Empty;
        public string Id => id;
    }
}
