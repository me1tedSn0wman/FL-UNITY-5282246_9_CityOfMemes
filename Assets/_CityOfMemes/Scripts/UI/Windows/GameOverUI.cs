using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : WindowUI
{
    [SerializeField] private Button button_ToMainMenu;

    public override void Awake() { 
        base.Awake();
        button_ToMainMenu.onClick.AddListener(() =>
        {
            GameManager.LOAD_MAIN_MENU_SCENE();
            SoundUI.Instance.TryPlayClickSound();
        });
    }
}
