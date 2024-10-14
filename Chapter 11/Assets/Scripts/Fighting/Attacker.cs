using System;
using UnityEngine;

namespace Fight
{
    public class Attacker : MonoBehaviour
    {
        public event Action AttackStarted;
        public event Action AttackEnded;

        public bool IsAttacking;
        [SerializeField]
        private DamageCauser _damageCauser;
        [SerializeField]
        private Animator _animator;

        public void Attack()
        {
            IsAttacking = true;
            _animator.SetTrigger("Slash");
            AttackStarted?.Invoke();
        }

        void SlashStart()
        {
            _damageCauser.EnableDamage();
        }

        void SlashEnd()
        {
            _damageCauser.DisableDamage();
            IsAttacking = false;
            AttackEnded?.Invoke();
        }
    }
}

