using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NPC : MonoBehaviour
{
    public Vector3 initRotation = new Vector3(90f, 0f, 0f);

    public GameObject dialogPlaneGO;
    public TextMeshPro text_Dialog;
    [TextArea]
    public string dialogText;

    public Transform playerTransform;

    public void Awake()
    {
        transform.localEulerAngles = initRotation;
        playerTransform = GameplayManager.Instance.playerTransform;
        dialogPlaneGO.SetActive(false);
    }

    public void Update()
    {
        
        LookAtPlayer();
    }

    public void LookAtPlayer() {
        Vector2 tPos1 = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.z - transform.position.z);
        Vector2 tPos2 = Vector2.right;
        float angl = Vector2.SignedAngle(tPos1, tPos2);
        Vector3 endRot = initRotation 
            + new Vector3(0, 90 + angl, 0);
        transform.localEulerAngles = endRot;
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
