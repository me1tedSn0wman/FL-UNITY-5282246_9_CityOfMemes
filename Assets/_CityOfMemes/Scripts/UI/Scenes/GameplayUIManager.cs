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
    [SerializeField] private Button button_Save;
    [SerializeField] private Button button_Load;
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
            SoundUI.Instance.TryPlayClickSound();
            gameplayManager.OnGameResume();
        });

        button_Save.onClick.AddListener(() =>
        {
            SoundUI.Instance.TryPlayClickSound();
            gameplayManager.saveManager.Save();
        });

        button_Load.onClick.AddListener(() =>
        {
            SoundUI.Instance.TryPlayClickSound();
            gameplayManager.saveManager.Load();
        });

        button_Settings.onClick.AddListener(() =>
        {
            SoundUI.Instance.TryPlayClickSound();
            settingsUI.SetActive(true);
        });

        button_ToMainMenu.onClick.AddListener(() =>
        {
            SoundUI.Instance.TryPlayClickSound();
            GameManager.LOAD_MAIN_MENU_SCENE();
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
