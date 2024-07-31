using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private PauseMenuHandler _pauseMenuHandler;
        private Button _quitButton;
        private Button _resumeButton;

        void OnEnable() 
        {
           _resumeButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("ResumeButton");
           _quitButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("QuitButton");
            _resumeButton.clicked += OnResumePressed;
            _quitButton.clicked += OnQuitPressed;
        }

        void OnDisable()
        {
            _resumeButton.clicked -= OnResumePressed;
            _quitButton.clicked -= OnQuitPressed;
        }

        void OnResumePressed()
        {
            _pauseMenuHandler.TogglePause();
        }

        void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}
