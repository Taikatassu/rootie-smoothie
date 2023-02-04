using System.Collections;
using System.Collections.Generic;
using RootieSmoothie.Audio;
using Unity.VisualScripting;
using UnityEngine;

namespace RootieSmoothie.UI
{
    [RequireComponent(typeof(ToggleButton))]
    public class AudioMuteToggle : MonoBehaviour
    {
        [SerializeField]
        private ToggleButton _btn;

        public void OnValidate()
        {
            _btn = GetComponent<ToggleButton>();
        }
        
        public void Start()
        {
            var muteValue = PlayerPrefs.GetInt("s_soundMuted");
            _btn.SetToggleState(muteValue > 0);
            _btn.OnValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(bool value)
        {
            AudioManager.Instance.SetSoundMuted(value);
        }
    }
}
