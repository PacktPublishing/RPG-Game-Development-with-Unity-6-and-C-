using UnityEngine;
using Fight;

namespace BasicFiniteStateMachine
{
    public class ExhaustionController : MonoBehaviour, IHaveExhaustion
    {
        [SerializeField] private float _initialExhaustionValue = 100;
        [SerializeField] private float _timeToHeal = 10;

        public float Power { get; private set; }

        public bool IsExhausted => Power <= 0;


        private float _timeSinceLastHeal;

        public void ConsumeExhaustion(float exhautionToConsume)
        {
            Power -= exhautionToConsume;
        }

        void Start()
        {
            Power = _initialExhaustionValue;
        }

        void Update()
        {
            _timeSinceLastHeal += Time.deltaTime;
            if (_timeSinceLastHeal >= _timeToHeal)
            {
                Power = _initialExhaustionValue;
                _timeSinceLastHeal = 0;
            }
        }
    }
}