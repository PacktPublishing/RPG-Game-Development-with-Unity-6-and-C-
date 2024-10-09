using UnityEngine;

namespace InventorySystem
{
    public class ItemDefinition : ScriptableObject 
    {
        public string Name;
        public Texture Icon;
        public string Description;

        public virtual ItemInstance MakeInstance()
        {
            return null;
        }
    }
}
