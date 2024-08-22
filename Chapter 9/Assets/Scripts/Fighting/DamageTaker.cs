using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class DamageTaker : MonoBehaviour
    {
        private List<IReceiveDamage> DamageReceivers = new();

        public void RegisterDamageReceiver(IReceiveDamage damageReceiver)
        {
            DamageReceivers.Add(damageReceiver);
        }

        void OnTriggerEnter(Collider _other)
        {
            if(_other.TryGetComponent(out DamageCauser damageCauser))
            {
                foreach(IReceiveDamage damageReceiver in DamageReceivers)
                {
                    damageReceiver.ReceiveDamage(damageCauser.DamageToCause);
                }
            }
        }
    }
}
