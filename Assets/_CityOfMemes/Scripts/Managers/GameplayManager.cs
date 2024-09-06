using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using YG;

public enum GameplayState { 
    Gameplay,
    Pause
}

public class GameplayManager : Singleton<GameplayManager>
{

    public GameplayUIManager gameplayUIManager;
    private PlayerControlManager playerControlManager;

    public static GameplayState gameplayState { get; private set; }
    public Transform playerTransform;
    public Vector3 playerPosOffset;


    [SerializeField] private int numOfKeys;
    [SerializeField] private int allNumOfKeys;

    [SerializeField] private PentagramTrigger pentagramTrigger;
    [SerializeField] private GameObject mishanyaGO;

    [SerializeField] private bool isCompassActive=false;
    [SerializeField] private Compass compass;
    [SerializeField] private List<GameObject> compassObjects;
    [SerializeField] private GameObject compassTriger;

    public override void Awake()
    {
        base.Awake();
        gameplayState = GameplayState.Gameplay;
        numOfKeys = 0;
    }

    public void Start()
    {
        playerControlManager = GameManager.Instance.playerControlManager;
        gameplayUIManager.gameplayManager = this;
        Subscribe();
        mishanyaGO.SetActive(false);

        
    }

    public void Update()
    {
        TryUpdateCompass();
    }

    public void PauseButtonPressed() {
        Debug.Log("Pause Button Pressed");
        switch (gameplayState) {
            case GameplayState.Gameplay:
                OnGamePause();
                break;
            case GameplayState.Pause:
                OnGameResume();
                break;
            default:
                break;
        }
    }

    public void OnGamePause() 
    {
        gameplayState = GameplayState.Pause;
        gameplayUIManager.SetActivePauseUI(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnGameResume()
    {
        gameplayState = GameplayState.Gameplay;
        gameplayUIManager.SetActivePauseUI(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Subscribe() {
        playerControlManager.OnPauseButtonPressed += PauseButtonPressed;
        pentagramTrigger.onEnter += TryActivateMish;
    }

    public void Unsubscribe() {
        playerControlManager.OnPauseButtonPressed -= PauseButtonPressed;
        pentagramTrigger.onEnter -= TryActivateMish;
    }

    public void TryActivateMish() {
        if (numOfKeys == allNumOfKeys) {
            mishanyaGO.SetActive(true);
            SoundUI.Instance.TryPlayFanfare();
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver() {
        yield return new WaitForSeconds(10f);
        gameplayUIManager.GameOver();
    }

    public void OnDestroy()
    {
        Unsubscribe();
    }

    public void AddKey() {
        numOfKeys++;
    }

    public void SetCompassActive(bool value) {
        compass.SetActive(value);
        isCompassActive = value;
    }

    public void TryUpdateCompass() {
        if (!isCompassActive) return;
        GameObject lastAvailableObject = null;
        float lastDistObject = float.MaxValue;
        for (int i = 0; i < compassObjects.Count; i++) {
            if (compassObjects[i] == null) {
                continue;
            }

            float crntDist = Vector3.Distance(playerTransform.position, compassObjects[i].transform.position);

            if (crntDist < lastDistObject) {
                lastDistObject = crntDist;
                lastAvailableObject = compassObjects[i];
            }
        }

        if (lastAvailableObject == null)
        {
            SetCompassActive(false);
        }
        else {
            compass.SetDestination(lastAvailableObject.transform.position);
        }
    }

    public void Reward() {
        SetCompassActive(true);
        compassTriger.SetActive(false);
    }

    public void TeleportPlayer(Vector3 newPos) { 
        playerTransform.position = newPos + playerPosOffset;
        SoundUI.Instance.TryPlayTeleport();
    }
}
