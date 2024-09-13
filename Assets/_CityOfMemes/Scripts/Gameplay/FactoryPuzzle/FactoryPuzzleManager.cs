using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryPuzzleManager : MonoBehaviour, ISaveable
{

    public bool isLocked;
    public bool isVictory;

    public float checkTime = 0.5f;

    public GameObject vsictoryKeyGO;

    public FactoryPuzzlePiece[] factoryPuzzlePieces;

    public void Awake()
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

    public string OnSave() {
        string data = (isVictory ? "1" : "0")
            + "%" + (isLocked ? "1" : "0");

        return data;
    }

    public void OnLoad(string data) {
        string[] datas = data.Split("%");

        if (datas.Length < 2) {
            Debug.LogError("Something Wrong with FactoryPuzzleManager Load");
        }

        isVictory = datas[0][0] == '1';
        isLocked = isVictory;
    }
}
