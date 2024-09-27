using Dialogue;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueCharacterController : MonoBehaviour
{
    [SerializeField] private DialogueController _dialogueController;
    [SerializeField] private DialogueSO _dialogue;
    [SerializeField] private GameObject _interactionCanvas;
    [SerializeField] private InputActionProperty _inputActionProperty;
    [SerializeField] private CinemachineCamera _cinemachineCamera;
    private TagHandle _tagHandle;
    private bool _isInside;

    private void Start()
    {
        _tagHandle = TagHandle.GetExistingTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag(_tagHandle))
        {
            return;
        }

        _isInside = true;
        ToggleActivation(true);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_tagHandle))
        {
            return;
        }

        _isInside = false;
        ToggleActivation(false);
    }

    private void ToggleActivation(bool isOn)
    {
        _interactionCanvas.SetActive(isOn);
        if (isOn)
        {
            _inputActionProperty.action.performed += OnInteractionPerformed;
        }
        else
        {
            _inputActionProperty.action.performed -= OnInteractionPerformed;
        }
    }

    private void OnInteractionPerformed(InputAction.CallbackContext obj)
    {
        _dialogueController.ShowDialogue(_dialogue, _cinemachineCamera);
        DialogueController.DialogueEnded += OnDialogueEnded;
        ToggleActivation(false);
    }

    private void OnDialogueEnded()
    {
        ToggleActivation(_isInside);
        DialogueController.DialogueEnded -= OnDialogueEnded;
    }
}
