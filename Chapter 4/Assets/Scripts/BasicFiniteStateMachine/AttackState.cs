using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class AttackState : IState
    {
        private Transform _playerTransform;
        private Animator _animator;
        private Actor _actor;

        public AttackState(Animator animator, Actor actor, Transform playerTransform)
        {
            _animator = animator;
            _actor = actor;
            _playerTransform = playerTransform;
        }

        public void OnEntry()
        {
            var lookRotation = Quaternion.LookRotation(_playerTransform.position - _actor.transform.position);
            _actor.transform.rotation = lookRotation;
            _animator.SetTrigger("Slash");
        }

        public void OnExit()
        {

        }

        public void OnUpdate()
        {
            if (Vector3.SqrMagnitude(_playerTransform.position - _animator.transform.position) < 3)
            {
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
                {
                    var lookRotation = Quaternion.LookRotation(_playerTransform.position - _actor.transform.position);
                    _actor.transform.rotation = lookRotation;
                    _animator.SetTrigger("Slash");
                }
            }
            else
            {
                _actor.SwitchToState(StateEnum.None);
            }
        }
    }
}
