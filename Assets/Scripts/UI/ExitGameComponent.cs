using UnityEngine;

namespace RootieSmoothie.UI
{
    [RequireComponent(typeof(BasicButton))]
    public class ExitGameComponent : MonoBehaviour
    {
        [SerializeField] private BasicButton _btn;

        public void OnValidate()
        {
            _btn = GetComponent<BasicButton>();
        }
        
        public void Start()
        {
            _btn.OnClickEvent.AddListener(OnClick);
        }

        private static void OnClick(BasicButton btn, Object obj)
        {
            Debug.LogWarning("Quit");
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
