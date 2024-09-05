using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzlePiece[] puzzlePieces;
    public bool isLocked;
    public bool isVictory;
    public float cellSize;

    public Vector2Int emptyGridPos;
    public float movingTime =0.5f;

    public GameObject dialogPlaneGO_before;
    public GameObject dialogPlaneGO_after;
    public GameObject vsictoryKeyGO;


    public void Start()
    {
        for (int i = 0; i < puzzlePieces.Length; i++) {
            puzzlePieces[i].puzzleManager = this;
            int num = i + 1;
            puzzlePieces[i].gridPos = new Vector2Int(num%4, num/4);
            puzzlePieces[i].SetPuzzlePiece();
        }
        vsictoryKeyGO.SetActive(false);
        dialogPlaneGO_before.SetActive(false);
        dialogPlaneGO_after.SetActive(false);
    }

    public Vector3 EmptyGridGlobalPos() {
        Vector3 endPos = new Vector3(-cellSize * emptyGridPos.x, -cellSize * emptyGridPos.y, 0);
        return endPos;
    }

    public void SetLocked(bool value) {
        if (true)
        {
            isLocked = value;
            StartCoroutine(WaitLocked());
        }
        else 
        { 
        
        }
    }

    IEnumerator WaitLocked() { 
        yield return new WaitForSeconds(movingTime);
        isLocked = false;
        CheckVictory();
    }

    public void CheckVictory() {
        for (int i = 0; i < puzzlePieces.Length; i++) {
            if (puzzlePieces[i].id != puzzlePieces[i].gridPos.x + 4 * puzzlePieces[i].gridPos.y)
                return;
        }
        Victory();
    }

    public void Victory()
    {
        isLocked = true;
        isVictory = true;
        if(vsictoryKeyGO!=null) vsictoryKeyGO.SetActive(true);
        Debug.Log("Puzzle Victory");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!isVictory)
                dialogPlaneGO_before.SetActive(true);
            else
                dialogPlaneGO_after.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isVictory)
                dialogPlaneGO_before.SetActive(false);
            else
                dialogPlaneGO_after.SetActive(false);
        }
    }
}
