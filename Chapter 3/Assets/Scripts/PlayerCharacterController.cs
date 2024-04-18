using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterController : MonoBehaviour
{
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
    private Vector2 _moveInput;
    private bool _isWalking;

    void Start()
    {
        _blockProperty.action.performed += OnBlockDown;
        _blockProperty.action.canceled += OnBlockUp;
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

        Vector3 transformDirection = _cameraTransform.TransformDirection(_moveInput);
        transformDirection.y = 0;
        Vector3 moveVector = transformDirection * _velocityFactor * Time.fixedDeltaTime;
        transform.LookAt(moveVector + transform.position);
        _characterController.Move(moveVector + (Physics.gravity * Time.fixedDeltaTime));
        _animator.SetBool("IsWalking", true);
        _isWalking = true;
    }

    void OnBlockDown(InputAction.CallbackContext callbackContext)
    {
        _animator.SetBool("IsBlocking", true);
    }

    
    void OnBlockUp(InputAction.CallbackContext callbackContext)
    {
        _animator.SetBool("IsBlocking", false);
    }

    void OnAttack(InputValue input)
    {
        if(_isWalking)
        {
            return;
        }

        _animator.SetTrigger("Slash");
    }

    void OnMove(InputValue input)
    {
        Vector2 result = input.Get<Vector2>();
        _moveInput = result;
    }
}
