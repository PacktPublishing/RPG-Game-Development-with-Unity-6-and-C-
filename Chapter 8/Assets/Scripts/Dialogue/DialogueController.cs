using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private DialogueSO _testDialogue;
        [SerializeField] private CinemachineCamera _testCameraTarget;
        
        public static event Action DialogueStarted, DialogueEnded;
        private CinemachineCamera _dialogueCameraTarget;

        private void Start()
        {
            _dialogueView.DialogueEnded += OnDialogueEnded;
        }

        public void ShowDialogue(DialogueSO dialogue, CinemachineCamera dialogueCameraTarget)
        {
            if(dialogueCameraTarget != null)
            {
            _dialogueCameraTarget = dialogueCameraTarget;
            _dialogueCameraTarget.gameObject.SetActive(true);
            }

            _dialogueView.LoadAndDisplayDialogue(dialogue);
            DialogueStarted?.Invoke();
        }

        [ContextMenu("Show Test Dialogue")]
        private void ShowTestDialogue()
        {
            ShowDialogue(_testDialogue, _testCameraTarget);
        }

        private void OnDialogueEnded()
        {
            DialogueEnded?.Invoke();
            if(_dialogueCameraTarget != null)
            {
                _dialogueCameraTarget.gameObject.SetActive(false);
                _dialogueCameraTarget = null;
            }
        }
    }
}