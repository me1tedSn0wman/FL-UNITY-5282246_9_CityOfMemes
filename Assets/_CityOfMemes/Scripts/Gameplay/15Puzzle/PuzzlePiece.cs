using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour, IInteractable
{
    public int id;
    public Vector2Int gridPos;

    public PuzzleManager puzzleManager;
    

    public void OnInteract() {
        if (puzzleManager.isLocked) return;

        if (false
            || (Mathf.Abs(gridPos.x - puzzleManager.emptyGridPos.x) == 1 && (gridPos.y - puzzleManager.emptyGridPos.y == 0))
            || (Mathf.Abs(gridPos.y - puzzleManager.emptyGridPos.y) == 1 && (gridPos.x - puzzleManager.emptyGridPos.x == 0))
            ) {
            SwapPos(puzzleManager.emptyGridPos);
        }
    }

    public void SwapPos(Vector2Int newPos) {
        transform.DOLocalMove(puzzleManager.EmptyGridGlobalPos(), puzzleManager.movingTime);
        Vector2Int t = gridPos;
        gridPos = puzzleManager.emptyGridPos;
        puzzleManager.emptyGridPos = t;
        puzzleManager.SetLocked(true);
    }

    public void SetPuzzlePiece() {
        transform.localPosition = new Vector3(-gridPos.x * puzzleManager.cellSize, -gridPos.y * puzzleManager.cellSize,0);
    }
}
