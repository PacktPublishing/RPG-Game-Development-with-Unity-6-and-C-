using UnityEngine;
using UnityEngine.InputSystem;

namespace RPGSample.Managers
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 MoveInput;
        public bool SprintInput;
        [SerializeField] private PlayerMovementManager playerMovementManager;

        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction sprintAction;
        public void UpdateInput()
        {
            MoveInput = moveAction.ReadValue<Vector2>();
            SprintInput = sprintAction.IsPressed();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            moveAction = InputSystem.actions.FindAction("Move");
            jumpAction = InputSystem.actions.FindAction("Jump");
            sprintAction = InputSystem.actions.FindAction("Sprint");
            jumpAction.performed += OnJumpActionPerformed;
        }

        private void OnJumpActionPerformed(InputAction.CallbackContext obj)
        {
            playerMovementManager.Jump();
        }
    }
}
