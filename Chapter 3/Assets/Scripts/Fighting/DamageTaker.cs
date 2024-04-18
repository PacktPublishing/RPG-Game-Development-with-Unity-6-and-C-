using UnityEngine;

namespace Fight
{
    public class DamageTaker : MonoBehaviour
    {
        void OnTriggerEnter(Collider _other)
        {
            if(_other.TryGetComponent<DamageCauser>(out DamageCauser damageCauser))
            {
                Debug.Log($"Damage Causer {damageCauser.DamageToCause}");
            }
        }
    }
}
