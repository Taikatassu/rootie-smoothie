using System;
using System.Collections;
using System.Collections.Generic;
using RootieSmoothie.Audio;
using RootieSmoothie.Headpats;
using UnityEngine;

namespace RootieSmoothie
{
    public class PattablePerson : MonoBehaviour, IPattablePerson
    {
        [SerializeField] private Transform _visualRoot;
        [SerializeField] private AudioClip _voice;
        private Double _lastTimestamp;

        public void OnPatStart()
        {
            _visualRoot.localScale = new Vector3(1f, 0.95f, 1f);
            if (Time.timeAsDouble - _lastTimestamp > 0.2)
            {
                var pitch = RandomHelper.GetRandomRange(0.85f, 1.1f);
                AudioManager.Instance.PlaySound(this, _voice, pitch: pitch);
            }
        }

        public void OnPatRelease()
        {
            _visualRoot.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
