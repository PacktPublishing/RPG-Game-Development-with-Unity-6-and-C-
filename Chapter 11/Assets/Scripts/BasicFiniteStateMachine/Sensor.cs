using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class Sensor : MonoBehaviour
    {
        public static bool IsPlayerAttacking { get; set; }
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _triggerDistance;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _triggerAngle;
        [SerializeField] private StateMachine _stateMachine;

        private void Update()
        {
            float distanceSquared = GetVectorBetweenPlayerAndActor().sqrMagnitude;

            if (distanceSquared >= _triggerDistance)
            {
                _stateMachine.SwitchState(StateEnum.None);
            }
            else if (distanceSquared > _attackDistance)
            {
                _stateMachine.SwitchState(StateEnum.Move);
            }
            else
            {
                if(IsPlayerAttacking)
                {
                    _stateMachine.SwitchState(StateEnum.Block);
                }
                else
                {
                    _stateMachine.SwitchState(StateEnum.Attack);
                }
            }
        }

        public Vector3 GetVectorBetweenPlayerAndActor()
        {
            return _playerTransform.transform.position - transform.position;
        }
    }
}