using UnityEngine;

public class Compass : MonoBehaviour
{
    public void SetDestination(Vector3 dest) { 
        gameObject.transform.LookAt(dest);
    }

    public void SetActive(bool value) { 
        gameObject.SetActive(value);
    }
}
