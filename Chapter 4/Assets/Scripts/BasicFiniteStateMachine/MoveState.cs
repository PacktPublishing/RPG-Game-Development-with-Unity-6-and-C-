using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class MoveState : IState
    {
        private const float moveSpeed = 1;
        private Actor _actor;
        private Transform _playerTransform;
        private CharacterController _actorCharacterController;
        private Animator _animator;

        public MoveState(Actor actor, Transform playerTransform, CharacterController characterController, Animator animator)
        {
            _actor = actor;
            _playerTransform = playerTransform;
            _actorCharacterController = characterController;
            _animator = animator;
        }

        public void OnEntry()
        {
             _animator.SetBool("IsWalking", true);
        }

        public void OnExit()
        {
             _animator.SetBool("IsWalking", false);
        }

        public void OnUpdate()
        {
            if((_playerTransform.position - _actor.transform.position).sqrMagnitude < 3)
            {
                _actor.SwitchToState(StateEnum.None);
            }
            else
            {
                var lookRotation = Quaternion.LookRotation(_playerTransform.position - _actor.transform.position);
                _actor.transform.rotation = lookRotation;
                _actorCharacterController.Move(_actor.transform.forward * moveSpeed * Time.deltaTime);
            }
        }
    }
}
