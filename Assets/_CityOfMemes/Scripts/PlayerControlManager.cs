using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlManager : MonoBehaviour
{
    public Vector2 move { get; private set; }
    public Vector2 look { get; private set; }
    public float sprint { get; private set; }
    public float jump { get; private set; }

    public Vector2 moveVal;
    public Vector2 lookVal;
    public float sprintVal;
    public float jumpVal;

    public Action OnPauseButtonPressed;

    private PlayerInputManager playerInputManager;

    public void Update()
    {
        moveVal = move;
        lookVal = look;
        sprintVal = sprint;
        jumpVal = jump;
    }

    public void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void OnMove(InputAction.CallbackContext context) {
        
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context) {
        if (GameManager.isMobile) return;
        look = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context) { 
        sprint = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context) { 
        jump = context.ReadValue<float>();
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (GameManager.Instance.gameState != GameState.Gameplay) return;
        OnPauseButtonPressed?.Invoke();
    }

    public void SetMove(Vector2 value) {
        move = value;
    }
    public void SetLook(Vector2 value) {
        look = value;
    }
    public void SetSprint(float value) {
        sprint = value;
    }
    public void SetJump(float value) {
        jump = value;
    }
}
