using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SnakeTrigger : MonoBehaviour
{
    public GameObject dialogPlaneGO;
    public TextMeshPro text_Dialog;

    public void Awake()
    {
        dialogPlaneGO.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            dialogPlaneGO.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            dialogPlaneGO.SetActive(false);
        }
    }
}
