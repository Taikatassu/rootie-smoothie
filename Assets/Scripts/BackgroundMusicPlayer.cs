using RootieSmoothie;
using RootieSmoothie.Audio;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicPlayer : Singleton<BackgroundMusicPlayer>
{
    [SerializeField] private AudioClip _gameplayMusic;

    public void Start()
    {
        AudioManager.Instance.PlaySound(this, _gameplayMusic, true);
    }
}
