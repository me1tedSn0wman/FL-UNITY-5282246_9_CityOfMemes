
using System;
using UnityEngine;

public class PentagramTrigger : MonoBehaviour
{
    public bool isTriggered = false;
    public event Action onEnter;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            onEnter.Invoke();
        }
    }

}
