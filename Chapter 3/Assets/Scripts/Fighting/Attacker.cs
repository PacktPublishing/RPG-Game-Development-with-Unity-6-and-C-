using UnityEngine;

namespace Fight
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField]
        private DamageCauser _damageCauser;
        void SlashStart()
        {
            _damageCauser.EnableDamage();
        }

        void SlashEnd()
        {
            _damageCauser.DisableDamage();
        }
    }
}

