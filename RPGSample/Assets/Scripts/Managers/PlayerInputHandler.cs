using UnityEngine;
using UnityEngine.InputSystem;

namespace RPGSample.Managers
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovementManager playerMovementManager;

        private InputAction moveAction;
        private InputAction jumpAction;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            moveAction = InputSystem.actions.FindAction("Move");
            jumpAction = InputSystem.actions.FindAction("Jump");
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            playerMovementManager.OnPlayerInput(moveValue);
        }
    }
}
