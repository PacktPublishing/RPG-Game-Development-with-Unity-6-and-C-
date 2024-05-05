using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class DamageTaker : MonoBehaviour
    {
        [SerializeField] private DamageCauser _damageCauserToIgnore;
        private List<IReceiveDamage> _damageReceivers = new();

        public void RegisterDamageReceiver(IReceiveDamage damageReceiver)
        {
            _damageReceivers.Add(damageReceiver);
        }

        void OnTriggerEnter(Collider _other)
        {
            if (_other.TryGetComponent(out DamageCauser damageCauser))
            {
                if(damageCauser != _damageCauserToIgnore)
                {
                    foreach(var damageReceiver in _damageReceivers)
                    {
                        damageReceiver.TakeDamage(damageCauser.DamageToCause);
                    }
                }
            }
        }
    }
}
