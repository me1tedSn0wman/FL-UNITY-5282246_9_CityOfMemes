using UnityEngine;
using UnityEngine.EventSystems;

public enum JoyStickButtonType{ 
    Move,
    Look,
}

public class JoyStickButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private JoyStickButtonType type;

    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject center;


    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform centerRectTransform;
    [SerializeField] private RectTransform aimRectTransform;

    private PlayerControlManager playerControlManager;

    public void Awake() { 
        rectTransform= GetComponent<RectTransform>();
        centerRectTransform =center.GetComponent<RectTransform>();
        aimRectTransform = aim.GetComponent<RectTransform>();
    }

    public void Start()
    {
        playerControlManager = GameManager.Instance.playerControlManager;
    }

    public void OnBeginDrag(PointerEventData eventData) { 

    }

    public void OnEndDrag(PointerEventData eventData) {

        aimRectTransform.anchoredPosition = new Vector2(
            0,
            0
            );

        switch (type) {
            case JoyStickButtonType.Move:
                playerControlManager.SetMove(new Vector2(0, 0));
                break;
            case JoyStickButtonType.Look:
                playerControlManager.SetLook(new Vector2(0, 0));
                break;
            }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerPosition = eventData.position;

        Vector2 centerPosition = centerRectTransform.position;

        Vector2 delta = new Vector2(pointerPosition.x - centerPosition.x, pointerPosition.y - centerPosition.y);
        Vector2 deltaNormalize = new Vector2(
            Mathf.Clamp(delta.x / (0.5f * rectTransform.rect.width), -1.0f, 1.0f),
            Mathf.Clamp(delta.y / (0.5f * rectTransform.rect.height), -1.0f, 1.0f)
            );

        aimRectTransform.anchoredPosition = new Vector2(
            Mathf.Clamp(delta.x, -0.5f * rectTransform.rect.width, 0.5f * rectTransform.rect.width),
            Mathf.Clamp(delta.y, -0.5f * rectTransform.rect.height, 0.5f * rectTransform.rect.height)
        );

        switch (type) {
            case JoyStickButtonType.Move:
                playerControlManager.SetMove(deltaNormalize);
                break;
            case JoyStickButtonType.Look:
                playerControlManager.SetLook(deltaNormalize);
                break;
        }
    }
}