using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Player")]
    public float moveSpeed = 4.0f;
    public float sprintSpeed = 7.5f;

    public float rotationSpeed = 1.0f;
    public float speedChangeRate = 10.0f;

    [Header("Jump")]
    public float jumpHeight = 1.2f;

    public float jumpTimeout = 0.1f;
    public float fallTimeout = 0.15f;

    // player
    private float _speed;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private Rigidbody playerRb;

    public LayerMask GroundLayers;

    [SerializeField] private float playerJumpForce;

    public PlayerControlManager playerControlManager;
    public Transform cameraTransform;

    Vector3 camRot;

    [SerializeField] private PlayerGroundCollision playerGroundCollision;

    [SerializeField] private float raycastLength;
    [SerializeField] private GameObject interactIcon;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        camRot = cameraTransform.rotation.eulerAngles;
    }

    private void Start()
    {
        playerControlManager = GameManager.Instance.playerControlManager;
        Subscribe();
    }

    private void Update()
    {
        if (GameplayManager.gameplayState == GameplayState.Gameplay)
        {
            TryLook();
            ShowInteract();
        }
    }

    private void FixedUpdate()
    {
        if (GameplayManager.gameplayState == GameplayState.Gameplay)
        {
            TryMove();
            TryJump();
            TryInteract();
        }
    }

    public void TryJump() {
        if (playerControlManager.jump == 0) return;
        if (!playerGroundCollision.isGrounded) return;
        playerRb.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);

    }

    public void TryMove() {
        float targetSpeed = playerControlManager.sprint > 0 ? sprintSpeed : moveSpeed;

        if (playerControlManager.move == Vector2.zero)
        {
            targetSpeed = 0f;
        }

        float currentHorizontalSpeed = new Vector3(
            playerRb.velocity.x,
            0.0f,
            playerRb.velocity.z
            ).magnitude;

        float speedOffset = 0.1f;
        Vector2 playerMoveNorm = playerControlManager.move.normalized;

        float inputMagnitude = playerMoveNorm.magnitude;

        /*
        if (false
            || currentHorizontalSpeed < targetSpeed - speedOffset
            || currentHorizontalSpeed > targetSpeed + speedOffset
            )
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else {
            _speed = targetSpeed * inputMagnitude;
        }
        */
        _speed = targetSpeed * inputMagnitude;

        if (playerControlManager.move != Vector2.zero) { 
           
        }
        Vector3 moveVal = new Vector3(playerMoveNorm.x, 0, playerMoveNorm.y);

        transform.Translate(transform.rotation * moveVal * _speed * Time.deltaTime, Space.World);
    }

    private void TryLook() {
        if (playerControlManager.look.sqrMagnitude < 0.01f)
            return;

        float scaledRotateSpeed = rotationSpeed * Time.deltaTime;

        RotateHorizontal(scaledRotateSpeed);
        RotateVertical(scaledRotateSpeed);

    }

    public void RotateHorizontal(float scaledRotateSpeed) {
        Vector3 rot = new Vector3(
            0,
            scaledRotateSpeed * playerControlManager.look.x,
            0
            );
        transform.Rotate(rot);
    }

    public void RotateVertical(float scaledRotateSpeed) {
        camRot = new Vector3(
            Mathf.Clamp(camRot.x - playerControlManager.look.y * scaledRotateSpeed, -89f,89f),
            0,
            0
            );
        //Debug.Log(rot);
        cameraTransform.localEulerAngles = camRot;
    }

    public void SetCameraSensitive(float value) {
        rotationSpeed = value;
    }

    public void Subscribe() {
        GameManager.OnCameraSensitiveChanged += SetCameraSensitive;
    }

    public void Unsubscribe() {
        GameManager.OnCameraSensitiveChanged -= SetCameraSensitive;
    }

    public void OnDestroy()
    {
        Unsubscribe();
    }

    public void TryInteract() {
        if (playerControlManager.interact == 0) return;
        int layerMask = 1 << 8;

        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, raycastLength, layerMask)) {
            if (hit.transform.CompareTag("Interactable")) {
                IInteractable inter = hit.transform.GetComponent<IInteractable>();
                if (inter != null) {
                    inter.OnInteract();
                    SoundUI.Instance.TryPlayClickSound();
                }
            }
        }
    }

    public void ShowInteract() {
        int layerMask = 1 << 8;
        RaycastHit hit;


        Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward)* raycastLength, Color.red, 0.0f);

        if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, raycastLength, layerMask))
        {
            if (hit.transform.CompareTag("Interactable"))
            {
                IInteractable inter = hit.transform.GetComponent<IInteractable>();
                if (inter != null)
                {
                    interactIcon.SetActive(true);
                    return;
                }
            }
        }
        interactIcon.SetActive(false);
    }

}
