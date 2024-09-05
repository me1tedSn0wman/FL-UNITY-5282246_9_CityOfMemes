using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryPuzzleManager : MonoBehaviour
{

    public bool isLocked;
    public bool isVictory;

    public float checkTime = 0.5f;

    public GameObject vsictoryKeyGO;

    public FactoryPuzzlePiece[] factoryPuzzlePieces;

    public void Start()
    {
        vsictoryKeyGO.SetActive(false);
        for (int i = 0; i < factoryPuzzlePieces.Length; i++) {
            factoryPuzzlePieces[i].puzzleManager = this;
        }
        isLocked = false;
    }

    public void SetLocked(bool value)
    {
        if (true)
        {
            isLocked = value;
            StartCoroutine(WaitLocked());
        }
        else
        {

        }
    }


    IEnumerator WaitLocked()
    {
        yield return new WaitForSeconds(checkTime);
        isLocked = false;
        CheckVictory();
    }

    public void CheckVictory()
    {
        for (int i = 0; i < factoryPuzzlePieces.Length; i++) {
            if (!factoryPuzzlePieces[i].IsRightIndex())
                return;

        }
        Victory();
    }

    public void Victory()
    {
        isLocked = true;
        isVictory = true;
        if (vsictoryKeyGO != null) vsictoryKeyGO.SetActive(true);
        Debug.Log("Factory Puzzle Victory");
    }

}
