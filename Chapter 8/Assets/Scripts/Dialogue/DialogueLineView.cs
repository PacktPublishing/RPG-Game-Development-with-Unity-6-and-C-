using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogue
{
    public class DialogueLineView : MonoBehaviour
    {
        [SerializeField] private DialogueCharacterDisplay _dialogueCharacterDisplay;
        private Label _lineTextDisplay;

        private void OnEnable()
        {
            _lineTextDisplay = GetComponent<UIDocument>().rootVisualElement.Q<Label>("LineDisplay");
        }

        public void ShowDialogueLine(DialogueLine dialogueLine)
        {

            _dialogueCharacterDisplay.LoadCharacter(dialogueLine.Character);
            _lineTextDisplay.text = dialogueLine.Line;
        }
    }
}