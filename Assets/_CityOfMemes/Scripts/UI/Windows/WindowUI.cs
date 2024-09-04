using UnityEngine;
using UnityEngine.UI;

public class WindowUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button button_CloseWindow;

    public virtual void Awake() {
        if (button_CloseWindow != null) {
            button_CloseWindow.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                TryPlayClickSound();
            });
        }
    }

    public virtual void SetActive(bool value) { 
        gameObject.SetActive(value);
    }

    public virtual void TryPlayClickSound() {
        SoundUI.Instance.TryPlayClickSound();
    }
}
