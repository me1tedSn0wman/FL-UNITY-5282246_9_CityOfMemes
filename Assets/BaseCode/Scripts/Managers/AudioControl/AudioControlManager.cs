using System;
using UnityEngine;
using Utils;
using YG;

public class AudioControlManager : Soliton<AudioControlManager>
{
    [SerializeField] private static float _soundVolume;
    public static float soundVolume
    {
        get { return _soundVolume; }
        set {
            _soundVolume = Mathf.Clamp01(value);
            OnSoundVolumeChanged?.Invoke(_soundVolume);
        }
    }
    public static event Action<float> OnSoundVolumeChanged;
    public static event Action OnPauseAudio;
    public static event Action OnResumeAudio;


    [SerializeField] private static float _musicVolume;
    public static float musicVolume
    {
        get { return _musicVolume; }
        set { 
            _musicVolume = Mathf.Clamp01(value);
            OnMusicVolumeChanged?.Invoke(_musicVolume);
        }
    }
    public static event Action<float> OnMusicVolumeChanged;

    public void SetSoundVolumeWithoutNotification(float value) {
        _soundVolume = value;
    }

    public void SetMusicVolumeWithoutNotification(float value)
    {
        _musicVolume = value;
    }

    public static void SubscribeYG() {
        YandexGame.onVisibilityWindowGame += PlayAll;
    }

    public static void UnsubscribeYG()
    {
        YandexGame.onVisibilityWindowGame -= PlayAll;
    }

    public static void PlayAll(bool value) {
        if (value)
        {
            OnResumeAudio?.Invoke();
        }
        else {
            OnPauseAudio?.Invoke();
        }
    }

    public void OnDestroy()
    {
        UnsubscribeYG();
    }
}
