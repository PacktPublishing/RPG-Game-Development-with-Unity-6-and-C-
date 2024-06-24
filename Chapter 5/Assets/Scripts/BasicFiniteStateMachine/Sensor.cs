using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class Sensor : MonoBehaviour
    {
        [SerializeField] private PlayerCharacterController _player;
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
                if(_player.IsAttacking())
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
            return _player.transform.position - transform.position;
        }
    }
}