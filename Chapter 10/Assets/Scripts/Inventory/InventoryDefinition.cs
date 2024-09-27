using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "InventoryDefinition", menuName = "Scriptable Objects/InventoryDefinition")]
    public class InventoryDefinition : ScriptableObject
    {
        public ItemDefinition[] ItemsInInventory;
        public int[] MultiplierPerItem;
        public int Credit;
    }
}
