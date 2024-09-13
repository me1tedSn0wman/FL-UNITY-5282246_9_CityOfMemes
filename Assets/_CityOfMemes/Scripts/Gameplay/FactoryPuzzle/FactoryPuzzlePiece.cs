using Unity.VisualScripting;
using UnityEngine;
using YG;

public class FactoryPuzzlePiece : MonoBehaviour, IInteractable, ISaveable
{
    public int endIndexValue;

    public FactoryPuzzleManager puzzleManager;

    public GameObject[] puzzleParts;
    public int activePuzzlePart = 0;

    public void Awake()
    {
        for (int i = 0; i < puzzleParts.Length; i++) {
            puzzleParts[i].SetActive(false);
        }
        puzzleParts[activePuzzlePart].SetActive(true);
    }

    public void OnInteract() {
        if (puzzleManager.isLocked) return;

        MoveToNextPuzzlePart();
    }

    public void MoveToNextPuzzlePart() {
        int newActivePart = (activePuzzlePart+1) % puzzleParts.Length;
        SetActivePuzzlePart(newActivePart);
        puzzleManager.SetLocked(true);
    }

    public void SetActivePuzzlePart(int index) {
        puzzleParts[activePuzzlePart].SetActive(false);
        puzzleParts[index].SetActive(true);
        activePuzzlePart = index;
    }

    public bool IsRightIndex() {
        return activePuzzlePart == endIndexValue;
    }

    public string OnSave() {
        string str = activePuzzlePart.ToString();
        return str;
    }

    public void OnLoad(string data) { 
        int index = int.Parse(data);

        if (index < puzzleParts.Length) {
            SetActivePuzzlePart(index);
        }
    }

}
