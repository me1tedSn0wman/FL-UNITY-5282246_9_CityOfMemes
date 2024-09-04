using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using YG;

public enum GameState { 
    MainMenu,
    Gameplay,
}

public class GameManager : Soliton<GameManager>
{
    public LangManager langManager;
    private GameState _gameState;

    public GameState gameState {
        get {
            return _gameState;
        }
        set {
            _gameState = value;
        }
    }

    public PlayerControlManager playerControlManager;

    public static bool isMobile;

    public static float cameraSensitive
    {
        get {
            return _cameraSensitive;
        }
        set { 
            _cameraSensitive = value;
            OnCameraSensitiveChanged?.Invoke(_cameraSensitive);
        }
    }

    private static float _cameraSensitive;

    public static event Action<float> OnCameraSensitiveChanged;

    public override void Awake()
    {
        base.Awake();
        langManager = new LangManager();

    }

    public void Start()
    {
        StartCoroutine(InitYandex());
        cameraSensitive = 100f;
        Subscribe();
    }

    IEnumerator InitYandex() {
        yield return new WaitForSeconds(0.1f);

        YandexGame.GameReadyAPI();
        LangManager.currLang = YandexGame.EnvironmentData.language;
        isMobile = !YandexGame.EnvironmentData.isDesktop;

    }

    public static void LOAD_GAMEPLAY_SCENE() {
        SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
        Instance.gameState = GameState.Gameplay;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void LOAD_MAIN_MENU_SCENE() {
        SceneManager.LoadScene("MainMenuScene");
        Instance.gameState = GameState.MainMenu;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Subscribe() {
        YandexGame.RewardVideoEvent += Reward;
    }

    public void Unsubscribe() {
        YandexGame.RewardVideoEvent -= Reward;
    }

    public void Reward(int id)
    {
        if (GameplayManager.instanceExists)
            GameplayManager.Instance.Reward();
    }

    public void OnDestroy()
    {
        Unsubscribe();
    }
}
