using YG;
using UnityEngine;

public class CompassTriger : MonoBehaviour, IInteractable
{
    private float rotatingSpeed = 10f;

    public void Start() { 
    }

    public void Update()
    {
        Rotating();
    }

    public void Rotating()
    {
        transform.Rotate(
            rotatingSpeed * Time.deltaTime,
            0,
            0,
            Space.Self);
    }

    public void OnInteract() {
        Cursor.lockState = CursorLockMode.None;
        YandexGame.RewVideoShow(0);
    }
}
