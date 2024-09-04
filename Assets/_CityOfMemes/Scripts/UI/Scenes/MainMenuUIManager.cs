using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private WindowUI aboutGameUI;

    [SerializeField] private Button button_StartGame;
    [SerializeField] private Button button_Settings;
    [SerializeField] private Button button_AboutGame;

    private void Start()
    {
        button_StartGame.onClick.AddListener(() =>
        {
            GameManager.LOAD_GAMEPLAY_SCENE();
            SoundUI.Instance.TryPlayClickSound();
        });

        button_Settings.onClick.AddListener(() =>
        {
            settingsUI.SetActive(true);
            SoundUI.Instance.TryPlayClickSound();
        });

        button_AboutGame.onClick.AddListener(() =>
        {
            aboutGameUI.SetActive(true);
            SoundUI.Instance.TryPlayClickSound();
        });
    }
}
