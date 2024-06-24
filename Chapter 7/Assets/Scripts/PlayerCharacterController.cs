using UnityEngine;
using UnityEngine.InputSystem;
using Fight;
using UI;
using Unity.Cinemachine;

public class PlayerCharacterController : MonoBehaviour, IBlocker
{
    public bool IsBlocking { get; private set; }
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private float _velocityFactor;
    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private InputActionProperty _blockProperty;
    [SerializeField]
    private PauseMenuHandler _pauseMenuHandler;
    [SerializeField]
    private CinemachineBrain _playerCamera;
    private Vector2 _moveInput;
    private bool _isWalking;
    private bool _isPaused;

    public bool IsAttacking()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Slash");
    }

    void Start()
    {
        _pauseMenuHandler.PauseToggled += OnPauseToggled;
        _blockProperty.action.performed += OnBlockDown;
        _blockProperty.action.canceled += OnBlockUp;
        ToggleMouseLock(true);
    }

    void FixedUpdate()
    {
        WalkUpdate();
    }

    void WalkUpdate()
    {
        if(_moveInput == Vector2.zero)
        {
            _animator.SetBool("IsWalking", false);
            _isWalking = false;
            return;
        }

        float cameraHeading = _cameraTransform.eulerAngles.y;
        Quaternion cameraControlledRotation = Quaternion.Euler(0, cameraHeading, 0);
        Vector3 inputDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        Vector3 transformDirection = cameraControlledRotation * inputDirection;
        Vector3 moveVector = transformDirection * _velocityFactor * Time.fixedDeltaTime;
        transform.LookAt(moveVector + transform.position);
        _characterController.Move(moveVector + (Physics.gravity * Time.fixedDeltaTime));
        _animator.SetBool("IsWalking", true);
        _isWalking = true;
    }

    void OnBlockDown(InputAction.CallbackContext callbackContext)
    {
        if(_isWalking || _isPaused)
        {
            return;
        }
        
        transform.LookAt((GetCameraRotationDirection() * Vector3.forward) + transform.position);
        _animator.SetBool("IsBlocking", true);
        IsBlocking = true;
    }

    
    void OnBlockUp(InputAction.CallbackContext callbackContext)
    { 
        _animator.SetBool("IsBlocking", false);
        IsBlocking = false;
    }

    void OnPauseToggled(bool newState)
    {
        _isPaused = newState;
        ToggleMouseLock(!newState);
        _playerCamera.enabled = !newState;
    }

    void ToggleMouseLock(bool isOn)
    {
        Cursor.lockState = isOn ? CursorLockMode.Locked : CursorLockMode.None;
    }

    void OnAttack(InputValue input)
    {
        if(_isWalking || _isPaused)
        {
            return;
        }

        transform.LookAt((GetCameraRotationDirection() * Vector3.forward) + transform.position);
        _animator.SetTrigger("Slash");
    }

    void OnMove(InputValue input)
    {
        if(_isPaused)
        {
            return;
        }

        Vector2 result = input.Get<Vector2>();
        _moveInput = result;
    }

    Quaternion GetCameraRotationDirection()
    {
        float cameraHeading = _cameraTransform.eulerAngles.y;
        return Quaternion.Euler(0, cameraHeading, 0);
    }
}