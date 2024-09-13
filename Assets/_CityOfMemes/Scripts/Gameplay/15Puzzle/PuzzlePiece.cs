using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour, IInteractable , ISaveable
{
    public int id;
    public Vector2Int gridPos;

    public PuzzleManager puzzleManager;
    
    public float cellSize;

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
        cellSize = puzzleManager.cellSize;
        MoveToGridPos();
    }

    public void MoveToGridPos() {
        transform.localPosition = new Vector3(-gridPos.x * cellSize, -gridPos.y * cellSize, 0);
    }

    public string OnSave() {
        string data = gridPos.x + "%" + gridPos.y;
        return data;
    }

    public void OnLoad(string data)
    {
        string[] datas = data.Split("%");

        if (datas.Length < 2) {
            Debug.LogError("Something wrong with save PuzzlePiece " + id);
            return;
        }
        gridPos.x = int.Parse(datas[0]); //Debug.Log(gameObject.name + "___" + gridPos.x);
        gridPos.y = int.Parse(datas[1]); //Debug.Log(gameObject.name + "___" + gridPos.y);
        MoveToGridPos();
    }
}
