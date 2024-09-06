using UnityEngine;

public class ChessPuzzlePiece : MonoBehaviour
{
    public ChessPuzzleManager chessPuzzleManager;
    [SerializeField] private bool isTeleportBack;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isTeleportBack)
        {
            GameplayManager.Instance.TeleportPlayer(chessPuzzleManager.startPoint);
        }
    }
}
