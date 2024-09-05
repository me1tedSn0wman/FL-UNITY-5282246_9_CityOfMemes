using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    private float rotatingSpeed=3f;

    public void Update()
    {
        Rotating();
    }

    public void Rotating() {
        transform.Rotate(
            0,
            0,
            rotatingSpeed * Time.deltaTime,
            Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            GameplayManager.Instance.AddKey();
            SoundUI.Instance.TryPlayPickUp();
            Destroy(gameObject);
        }
    }
}
