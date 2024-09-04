using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : WindowUI
{
    [Header("Sliders")]
    [SerializeField] private Slider slider_SoundVolume;
    [SerializeField] private Slider slider_MusicVolume;
    [SerializeField] private Slider slider_CameraSensitive;

    [Header("Textes")]
    [SerializeField] private TextMeshProUGUI text_SoundVolume;
    [SerializeField] private TextMeshProUGUI text_MusicVolume;
    [SerializeField] private TextMeshProUGUI text_CameraSens;

    [Header("Dropdown")]
    [SerializeField] private TMP_Dropdown dropdown_Language;

    public override void Awake() {
        base.Awake();

        slider_SoundVolume.onValueChanged.AddListener((value) =>
        {
            AudioControlManager.soundVolume = value;
            text_SoundVolume.text = ((int)(value * 100)).ToString();
            TryPlayClickSound();
        });

        slider_MusicVolume.onValueChanged.AddListener((value) =>
        {
            AudioControlManager.musicVolume = value;
            text_MusicVolume.text = ((int)(value * 100)).ToString();
            TryPlayClickSound();
        });
        slider_CameraSensitive.onValueChanged.AddListener((value) =>
        {
            GameManager.cameraSensitive = value;
            text_CameraSens.text = ((int)value).ToString();
        });

        dropdown_Language.onValueChanged.AddListener((value) =>
        {
            SetLanguage(value);
            TryPlayClickSound();
        });

        UpdateInitValues();
    }

    public void UpdateInitValues() {
        float tSoundVol = AudioControlManager.soundVolume;
        float tMusicVol = AudioControlManager.musicVolume;
        float tSens = GameManager.cameraSensitive;

        slider_SoundVolume.SetValueWithoutNotify(tSoundVol);
        slider_MusicVolume.SetValueWithoutNotify(tMusicVol);
        slider_CameraSensitive.SetValueWithoutNotify(tSens);

        text_SoundVolume.text = ((int)(tSoundVol * 100)).ToString();
        text_MusicVolume.text = ((int)(tMusicVol * 100)).ToString();
        text_CameraSens.text = ((int)tSens).ToString();

        dropdown_Language.SetValueWithoutNotify(0);
    }

    public void SetLanguage(int value) {
        switch (value) {
            case 0:
                break;
            case 1:
                break;
        }
    }
}
