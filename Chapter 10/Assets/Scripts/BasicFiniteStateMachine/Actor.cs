using Fight;
using UnityEngine;
using UnityEngine.AI;

namespace BasicFiniteStateMachine
{
    public class Actor : MonoBehaviour, IBlocker
    {
        [SerializeField] private ExhaustionController _exhaustionController;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _speed = 1;
        [SerializeField] private Animator _animator;
        [SerializeField] private Sensor _sensor;
        [SerializeField] private Attacker _attacker;

        public bool IsBlocking { get; private set; }

        public void MoveTowardsPlayer()
        {
            LookTowardsPlayer();
            _navMeshAgent.Move(transform.forward * _speed * Time.deltaTime);
        }

        public void LookTowardsPlayer()
        {
            transform.rotation = Quaternion.LookRotation(_sensor.GetVectorBetweenPlayerAndActor());
        }

        public void SetIsWalking(bool value)
        {
            _animator.SetBool("IsWalking", value);
        }

        public void SetIsBlocking(bool value)
        {
            if(_exhaustionController.IsExhausted)
            {
                return;
            }

            if (value && !IsBlocking)
            {
                _exhaustionController.ConsumeExhaustion(10);
            }

            _animator.SetBool("IsBlocking", value);
            IsBlocking = value;
        }

        public void Attack()
        {
            if(_exhaustionController.IsExhausted)
            {
                return;
            }

            _attacker.Attack();
        }

        public bool IsAttacking()
        {
            return _attacker.IsAttacking;
        }
    }
}
