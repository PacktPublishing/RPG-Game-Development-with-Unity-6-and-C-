using UnityEngine;

namespace Fight
{
    public class DamageCauser : MonoBehaviour
    {
        [SerializeField]
        private Collider _collider;

        public int DamageToCause = 10;

        public void EnableDamage()
        {
            _collider.enabled = true;
        }

        public void DisableDamage()
        {
            _collider.enabled = false;
        }
    }
}

