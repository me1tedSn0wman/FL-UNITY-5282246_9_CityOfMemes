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
    public static bool isLoadOnGameplay;
    public static bool dataIsLoaded;

    public override void Awake()
    {
        base.Awake();
        isLoadOnGameplay = false;
        langManager = new LangManager();
        YandexGame.GetDataEvent += GetLoadDataYG;
    }

    public void Start()
    {
        //       StartCoroutine(InitYandexCor());
        InitYandex();
        cameraSensitive = 100f;
        Subscribe();
    }

    IEnumerator InitYandexCor() {
        yield return new WaitForSeconds(0.1f);

        InitYandex();
    }

    public void InitYandex() {
        YandexGame.GameReadyAPI();
        LangManager.currLang = YandexGame.EnvironmentData.language;
        isMobile = !YandexGame.EnvironmentData.isDesktop;
        AudioControlManager.SubscribeYG();
    }

    public static void LOAD_GAMEPLAY_SCENE(bool loadGame) {
        isLoadOnGameplay = loadGame;
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

    public void SaveGameData()
    {
#if PLATFORM_WEBGL
        SaveDataYG();
#endif
    }

    public void SaveDataYG()
    {
        YandexGame.SaveProgress();
    }

    private void GetLoadDataYG() {
        dataIsLoaded = true;
    }

    public void OnDestroy()
    {
        Unsubscribe();
    }
}
