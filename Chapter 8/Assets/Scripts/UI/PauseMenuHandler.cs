using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenuHandler : MonoBehaviour
    {
        public static event Action<bool> PauseToggled;
        [SerializeField] InputActionProperty _inputAction;
        [SerializeField] UIDocument _pauseUIDocument;

        void Start() => _inputAction.action.performed += OnInputActionPerformed;
        void OnDestroy() => _inputAction.action.performed -= OnInputActionPerformed;

        public void TogglePause()
        {
            bool newState = !_pauseUIDocument.gameObject.activeSelf;
            _pauseUIDocument.gameObject.SetActive(newState);
            PauseToggled?.Invoke(newState);
        }

        void OnInputActionPerformed(InputAction.CallbackContext context)
        {
            TogglePause();
        }
    }

}
