using UnityEngine;

public enum GroundCollisionState { 
    None,
    Ground,
}

public class PlayerGroundCollision : MonoBehaviour
{
    public GroundCollisionState groundCollisionState;

    public LayerMask groundMask;

    public bool isGrounded
    {
        get {
            switch (groundCollisionState) {
                case GroundCollisionState.Ground:
                    return true;
                case GroundCollisionState.None:
                    return false;
                default:
                    return false;
            }
        }
    }

    private void Awake()
    {
        groundCollisionState = GroundCollisionState.Ground;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerStay(Collider other)
    {

//        Debug.Log(other.gameObject.layer + "|||" + groundMask.value + "|||" + (other.gameObject.layer & groundMask.value));
        if (false
            || (groundMask.value >> other.gameObject.layer & 1) > 0
            )
        {
            groundCollisionState = GroundCollisionState.Ground;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (false
            || (groundMask.value >> other.gameObject.layer & 1) > 0
            ) 
        {

        }
        groundCollisionState = GroundCollisionState.None;
    }
}
