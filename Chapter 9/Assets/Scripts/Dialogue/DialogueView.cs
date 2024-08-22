using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogue
{
    public class DialogueView : MonoBehaviour 
    {
        public event Action DialogueEnded;
        [SerializeField] private DialogueLineView _dialogueLineView;
        [SerializeField] private UIDocument _documentUI;

        private DialogueSO _currentDialogue;
        private int _currentIndex;
        private Button _button;

        private void OnEnable()
        {
            _button = _documentUI.rootVisualElement.Q<Button>("ShowNextButton");
            _button.clicked += NextLine;
        }

        private void OnDisable()
        {
            _button.clicked -= NextLine;
        }

        public void LoadAndDisplayDialogue(DialogueSO dialogue)
        {
            _currentIndex = 0;
            _currentDialogue = dialogue;
            ShowUI();
            ShowCurrentLineOrHideUI();
        }

        private void ShowCurrentLineOrHideUI()
        {
            if (_currentDialogue.TryGetDialogueLineAt(_currentIndex, out DialogueLine line))
            {
                _dialogueLineView.ShowDialogueLine(line);
            }
            else
            {
                HideUI();
                DialogueEnded?.Invoke();
            }
        }

        private void NextLine()
        {
            _currentIndex++;
            ShowCurrentLineOrHideUI();
        }

        private void ShowUI()
        {
            _documentUI.gameObject.SetActive(true);
        }

        private void HideUI()
        {
            _documentUI.gameObject.SetActive(false);
        }
    }
}