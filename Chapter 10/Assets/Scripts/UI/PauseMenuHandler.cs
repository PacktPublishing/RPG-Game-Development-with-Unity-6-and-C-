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
        private bool _canPause = true;

        void Start() 
        {
            _inputAction.action.performed += OnInputActionPerformed;
            StoreUIView.StoreOpened += EnablePausing;
            StoreUIView.StoreClosed += DisablePausing;
        }
        void OnDestroy() => _inputAction.action.performed -= OnInputActionPerformed;

        public void TogglePause()
        {
            if(!_canPause)
            {
                return;
            }

            bool newState = !_pauseUIDocument.gameObject.activeSelf;
            _pauseUIDocument.gameObject.SetActive(newState);
            PauseToggled?.Invoke(newState);
        }

        void OnInputActionPerformed(InputAction.CallbackContext context)
        {
            TogglePause();
        }

        void DisablePausing()
        {
            _canPause = false;
        }

        void EnablePausing()
        {
            _canPause = true;
        }
    }

}
