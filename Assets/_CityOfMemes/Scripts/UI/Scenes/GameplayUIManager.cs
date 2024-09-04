using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] private Transform transform_PauseUI;
    [SerializeField] private GameObject canvas_GameplayUI;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private GameOverUI gameOverUI;

    [SerializeField] private Button button_Menu;
    [SerializeField] private Button button_Resume;
    [SerializeField] private Button button_Settings;
    [SerializeField] private Button button_ToMainMenu;

    [HideInInspector] public GameplayManager gameplayManager;

    private void Start()
    {
        button_Menu.onClick.AddListener(() =>
        {
            SetActivePauseUI(true);
            SoundUI.Instance.TryPlayClickSound();
        });

        button_Resume.onClick.AddListener(() =>
        {
            gameplayManager.OnGameResume();
            SoundUI.Instance.TryPlayClickSound();
        });

        button_Settings.onClick.AddListener(() =>
        {
            settingsUI.SetActive(true);
            SoundUI.Instance.TryPlayClickSound();
        });

        button_ToMainMenu.onClick.AddListener(() =>
        {
            GameManager.LOAD_MAIN_MENU_SCENE();
            SoundUI.Instance.TryPlayClickSound();
        });

        if (!GameManager.isMobile)
        {
            canvas_GameplayUI.SetActive(false);
        }
        else {
            canvas_GameplayUI.SetActive(true);
        }
    }

    public void SetActivePauseUI(bool value) {
        Debug.Log("Set Pause Value: " + value);
        transform_PauseUI.gameObject.SetActive(value);
    }

    public void GameOver() {
        gameOverUI.SetActive(true);
    }
}
