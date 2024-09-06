using UnityEngine;

public class FactoryPuzzlePiece : MonoBehaviour, IInteractable
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
        puzzleParts[activePuzzlePart].SetActive(false);
        puzzleParts[newActivePart].SetActive(true);
        activePuzzlePart = newActivePart;
        puzzleManager.SetLocked(true);
    }

    public bool IsRightIndex() {
        return activePuzzlePart == endIndexValue;
    }
}
