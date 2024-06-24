using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenuHandler : MonoBehaviour
    {
        public event Action<bool> PauseToggled;
        [SerializeField] InputActionProperty _inputAction;
        [SerializeField] UIDocument _pauseUIDocument;

        public void TogglePause()
        {
            bool newState = !_pauseUIDocument.gameObject.activeSelf;
            _pauseUIDocument.gameObject.SetActive(newState);
            PauseToggled?.Invoke(newState);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _inputAction.action.performed += OnInputActionPerformed;
        }

        void OnDestroy() 
        {
            _inputAction.action.performed -= OnInputActionPerformed;
        }

        void OnInputActionPerformed(InputAction.CallbackContext context)
        {
            TogglePause();
        }
    }

}
