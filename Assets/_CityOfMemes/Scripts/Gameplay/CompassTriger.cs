using YG;
using UnityEngine;

public class CompassTriger : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            YandexGame.RewVideoShow(0);
        }
    }
}
