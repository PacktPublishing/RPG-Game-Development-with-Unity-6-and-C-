using BasicFiniteStateMachine;
using UnityEngine;

namespace Fight
{
    public class Attacker : MonoBehaviour
    {
        public bool IsAttacking;
        [SerializeField]
        private DamageCauser _damageCauser;
        [SerializeField]
        private ExhaustionController _exhaustionController;
        [SerializeField]
        private Animator _animator;

        public void Attack()
        {
            IsAttacking = true;
            _animator.SetTrigger("Slash");
        }

        void SlashStart()
        {
            _damageCauser.EnableDamage();
        }

        void SlashEnd()
        {
            _damageCauser.DisableDamage();
            IsAttacking = false;
        }

        void OnAttackStarted()
        {
            if(_exhaustionController)
            {
                _exhaustionController.ConsumeExhaustion(10);
            }
        }
    }
}

