using BasicFiniteStateMachine;
using UnityEngine;

namespace Fight
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField]
        private DamageCauser _damageCauser;
        [SerializeField]
        private ExhaustionController _exhaustionController;
        void SlashStart()
        {
            _damageCauser.EnableDamage();
        }

        void SlashEnd()
        {
            _damageCauser.DisableDamage();
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

