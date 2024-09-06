using UnityEngine;

public class ChessPuzzleManager : MonoBehaviour
{
    [SerializeField] private ChessPuzzlePiece[] chessPuzzlePieces;
    [SerializeField] private Transform transform_StartPoint;

    public Vector3 startPoint {
        get {
            return transform_StartPoint.position;
        }
    }

    public void Start()
    {
        for (int i = 0; i < chessPuzzlePieces.Length; i++) {
            chessPuzzlePieces[i].chessPuzzleManager = this;
        }
    }
}
