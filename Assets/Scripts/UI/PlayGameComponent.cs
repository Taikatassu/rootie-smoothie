using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RootieSmoothie.UI
{
    [RequireComponent(typeof(BasicButton))]
    public class PlayGameComponent : MonoBehaviour
    {
        [SerializeField] private BasicButton _btn;
        [SerializeField] private string _gameSceneName = "CoreLoopDev";
        
        public void OnValidate()
        {
            _btn = GetComponent<BasicButton>();
            
        }

        public void Start()
        {
            _btn.OnClickEvent.AddListener(OnClick);
        }

        private void OnClick(BasicButton btn, Object obj)
        {
            SceneManager.LoadScene("CoreLoopDev");
        }
    }
}
