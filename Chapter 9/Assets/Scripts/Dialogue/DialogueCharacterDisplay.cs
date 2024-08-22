using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogue
{
    public class DialogueCharacterDisplay : MonoBehaviour 
    {
        private Image _characterImage;
        private Label _characterNameDisplay;

        private void OnEnable()
        {
            _characterImage = GetComponent<UIDocument>().rootVisualElement.Q<Image>("CharacterImage");
            _characterNameDisplay = GetComponent<UIDocument>().rootVisualElement.Q<Label>("CharacterName");
        }

        public void LoadCharacter(DialogueCharacterSO dialogueCharacter)
        {
            _characterImage.image = dialogueCharacter.Photo;
            _characterNameDisplay.text = dialogueCharacter.Name;
        }
    }
}