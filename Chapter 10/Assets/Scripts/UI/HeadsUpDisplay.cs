using BasicFiniteStateMachine;
using UnityEngine;
using UnityEngine.UIElements;

public class HeadsUpDisplay : MonoBehaviour
{
    private ProgressBar _progressBar;
    [SerializeField] private LifeController _playerLifeController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _progressBar = GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>();
        _playerLifeController.HealthChanged += OnHealthChanged;
        _progressBar.highValue = _playerLifeController.Health;
        _progressBar.value = _playerLifeController.Health;
        _progressBar.lowValue = 0;
    }

    void OnHealthChanged()
    {
        _progressBar.schedule.Execute(() =>
        {
            _progressBar.value = _progressBar.value + (_playerLifeController.Health < _progressBar.value ? -1 : 1);
        }).Every(10).Until(() => _progressBar.value == _playerLifeController.Health);
    }

}