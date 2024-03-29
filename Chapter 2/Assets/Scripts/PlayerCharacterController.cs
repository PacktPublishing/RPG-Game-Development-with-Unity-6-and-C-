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
    private Vector2 _moveInput;

    void FixedUpdate()
    {
        if(_moveInput == Vector2.zero)
        {
            _animator.SetBool("IsWalking", false);
            return;
        }

        Vector3 target = new Vector3(_moveInput.x, 0, _moveInput.y) * (_velocityFactor * Time.fixedDeltaTime);
        transform.LookAt(target + transform.position);
        _characterController.Move(target + (Physics.gravity * Time.fixedDeltaTime));
        _animator.SetBool("IsWalking", true);
    }

    void OnMove(InputValue input)
    {
        Vector2 result = input.Get<Vector2>();
        _moveInput = result;
    }
}
