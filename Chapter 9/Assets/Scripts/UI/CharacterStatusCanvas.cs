using BasicFiniteStateMachine;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusCanvas : MonoBehaviour
{
    [SerializeField] private LifeController _lifeController;
    [SerializeField] private ExhaustionController _exhaustionController;
    [SerializeField] private Image _healthForeground;
    [SerializeField] private Image _exhaustionForeground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lifeController.HealthChanged += OnHealthChanged;
        _exhaustionController.OnPowerChanged += OnPowerChanged;
    }

    private void OnPowerChanged()
    {
        SetForegroundCoverageArea(_exhaustionForeground, _exhaustionController.Power, _exhaustionController.MaxPower);
    }

    private void OnHealthChanged()
    {
        SetForegroundCoverageArea(_healthForeground, _lifeController.Health, _lifeController.MaxHealth);
    }

    private void SetForegroundCoverageArea(Image target, float currentValue, float maxValue)
    {
        var offsetMin = target.rectTransform.offsetMin;
        var percentage = currentValue / maxValue;
        offsetMin.x = Mathf.Lerp(1, 0, percentage);
        target.rectTransform.offsetMin = offsetMin;
    }
}
