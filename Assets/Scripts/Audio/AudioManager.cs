using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RootieSmoothie.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        private List<(Object obj, AudioSource source, string clipName)> _activePlayers = new();
        [SerializeField]
        private readonly Stack<AudioSource> _pool = new();
        private List<AudioSource> _sources = new();

        private AudioSource GetAudioSource()
        {
            if (_pool.Count > 0)
                return _pool.Pop();
            var obj = new GameObject("AudioSource");
            var audioSource = obj.AddComponent<AudioSource>();
            obj.transform.SetParent(transform);
            return audioSource;
        }

        public void PlaySound(Object obj, AudioClip clip, bool loop = false, float pitch = 1f)
        {
            if (clip == null)
                return;
            var source = GetAudioSource();
            source.clip = clip;
            source.loop = loop;
            source.pitch = pitch;
            source.Play();
            _activePlayers.Add((obj, source, clip.name));
            _sources.Add(source);
        }

        public void StopSound(Object obj, AudioClip clip)
        {
            var count = _activePlayers.Count;
            for (var i = count-1; i >= 0; --i)
            {
                var player = _activePlayers[i];
                if (player.obj != obj || !string.Equals(player.clipName, clip.name))
                    continue;
                player.source.Stop();
                _pool.Push(player.source);
                _activePlayers.RemoveAt(i);
                return;
            }
        }

        public void LateUpdate()
        {
            var count = _activePlayers.Count;
            for (var i = count - 1; i >= 0; --i)
            {
                var player = _activePlayers[i];
                if (player.source.isPlaying)
                    continue;
                _activePlayers.Remove(player);
            }
        }
    }
}
