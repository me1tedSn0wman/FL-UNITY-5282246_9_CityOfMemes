using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ScreenButtonType { 
    Sprint,
    Jump,
    Interact
}

public class ScreenButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private ScreenButtonType type;

    private Button button;

    private PlayerControlManager playerControlManager;

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
        });
    }

    public void Start()
    {
        this.playerControlManager = GameManager.Instance.playerControlManager;
    }

    public void OnPointerDown(PointerEventData eventData) {
        OnClick();
    }

    public void OnPointerUp(PointerEventData eventData) {
        OnRelease();
    }

    public void OnPointerExit(PointerEventData eventData) {
        OnRelease();
    }

    public void OnClick() {
        switch (type) { 
            case ScreenButtonType.Sprint:
                playerControlManager.SetSprint(1f);
                break;
            case ScreenButtonType.Jump:
                playerControlManager.SetJump(1f);
                break;
            case ScreenButtonType.Interact:
                playerControlManager.SetInteract(1f);
                break;
        }
    }

    public void OnRelease() {
        switch (type) {
            case ScreenButtonType.Sprint:
                playerControlManager.SetSprint(0f);
                break;
            case ScreenButtonType.Jump:
                playerControlManager.SetJump(0f);
                break;
            case ScreenButtonType.Interact:
                playerControlManager.SetInteract(0f);
                break;
        }
    }
}
