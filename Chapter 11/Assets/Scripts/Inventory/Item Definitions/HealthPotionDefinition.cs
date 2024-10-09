using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "HealthDefinition", menuName = "Scriptable Objects/HealthDefinition")]
    public class HealthPotionDefinition : ItemDefinition
    {
        public int AddedHealth;

        public override ItemInstance MakeInstance()
        {
            return new HealthPotionInstance(AddedHealth, this);
        }
    }
}
