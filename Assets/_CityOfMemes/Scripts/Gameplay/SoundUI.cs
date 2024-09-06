using UnityEngine;
using Utils;

public class SoundUI : Soliton<SoundUI>
{
    [SerializeField] public AudioClip clickSound;
    [SerializeField] public AudioClip fanfareSound;
    [SerializeField] public AudioClip pickUpSound;
    [SerializeField] public AudioClip teleportSound;
    [SerializeField] public AudioClip jumpSound;

    [Header("AudioControl")]
    [SerializeField] public AudioControl musicAudioControl;
    [SerializeField] public AudioControl soundAudioControl;

    public void Start()
    {
        AudioControlManager.musicVolume = 0.1f;
        AudioControlManager.soundVolume = 0.1f;
    }

    public void TryPlayClickSound() {
        if (soundAudioControl != null && clickSound != null) {
            soundAudioControl.PlayOneShot(clickSound);
        }
    }

    public void TryPlayFanfare() {
        if (soundAudioControl != null && fanfareSound != null) {
            soundAudioControl.PlayOneShot(fanfareSound);
        }
    }

    public void SetUpMusic(AudioClip audioClip) {
        if (musicAudioControl == null) return;
        musicAudioControl.PlayEndless(audioClip);
    }

    public void TryPlayPickUp() {
        if (soundAudioControl != null && pickUpSound != null) {
            soundAudioControl.PlayOneShot(pickUpSound);
        }
    }

    public void TryPlayTeleport() {
        if (soundAudioControl != null && teleportSound != null)
        {
            soundAudioControl.PlayOneShot(teleportSound);
        }
    }

    public void TryPlayJump()
    {
        if (soundAudioControl != null && jumpSound != null)
        {
//            soundAudioControl.PlayOneShot(jumpSound);
        }
    }
}
