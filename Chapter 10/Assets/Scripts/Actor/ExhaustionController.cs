using UnityEngine;
using Fight;
using System;

namespace BasicFiniteStateMachine
{
    public class ExhaustionController : MonoBehaviour, IHaveExhaustion
    {
        public float MaxPower => _initialExhaustionValue;
        [SerializeField] private float _initialExhaustionValue = 100;
        [SerializeField] private float _timeToHeal = 10;

        public float Power { get; private set; }

        public bool IsExhausted => Power <= 0;


        private float _timeSinceLastHeal;

        public event Action OnPowerChanged;

        public void ConsumeExhaustion(float exhautionToConsume)
        {
            Power -= exhautionToConsume;
            OnPowerChanged?.Invoke();
        }

        void Start()
        {
            Power = _initialExhaustionValue;
            OnPowerChanged?.Invoke();
        }

        void Update()
        {
            _timeSinceLastHeal += Time.deltaTime;
            if (_timeSinceLastHeal >= _timeToHeal)
            {
                Power = _initialExhaustionValue;
                _timeSinceLastHeal = 0;
                OnPowerChanged?.Invoke();
            }
        }
    }
}