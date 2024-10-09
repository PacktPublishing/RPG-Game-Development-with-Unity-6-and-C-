using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem
{
    public class Inventory
    {
        public event Action ItemsUpdated;
        public Dictionary<ItemDefinition, int> ItemsCountByDefinition
        {
            get;
            set;
        }

        public List<ItemDefinition> ItemsList
        {
            get;
            set;
        }

        public int Credits;
        private readonly Dictionary<int, ItemInstance> currentItems;

        public Inventory(InventoryDefinition inventoryDefinition)
        {
            ItemsCountByDefinition = new();
            currentItems = new();
            ItemsList = new();

            for (int i = 0; i < inventoryDefinition.ItemsInInventory.Length;i++)
            {
                for(int j = 0; j < inventoryDefinition.MultiplierPerItem[i]; j++)
                {
                    ItemInstance itemInstance = inventoryDefinition.ItemsInInventory[i].MakeInstance();
                    AddItemToContainers(itemInstance);
                }
            }

            Credits = inventoryDefinition.Credit;
            ItemsUpdated?.Invoke();
        }

        public void AddCredit(int creditToAdd)
        {
            Credits += creditToAdd;
            ItemsUpdated?.Invoke();
        }

        public bool RemoveCredit(int creditToRemove)
        {
            if(Credits < creditToRemove)
            {
                return false;
            }

            Credits -= creditToRemove;
            ItemsUpdated?.Invoke();
            return true;
        }

        public void AddItem(ItemInstance itemInstance)
        {
            AddItemToContainers(itemInstance);
            ItemsUpdated?.Invoke();
        }

        public void RemoveItem(int itemId)
        {
            ItemDefinition definition = currentItems[itemId].ItemDefinition;
            currentItems.Remove(itemId);

            ItemsCountByDefinition[definition]--;
            if (ItemsCountByDefinition[definition] <= 0)
            {
                ItemsCountByDefinition.Remove(definition);
                ItemsList.Remove(definition);
            }

            ItemsUpdated?.Invoke();
        }

        public ItemInstance GetItemOfTypeDefinition(ItemDefinition itemDefinition)
        {
            return currentItems.Values.Where(item => item.ItemDefinition == itemDefinition).FirstOrDefault();
        }

        private void AddItemToContainers(ItemInstance itemInstance)
        {
            currentItems.Add(itemInstance.ItemId, itemInstance);
            if (ItemsCountByDefinition.TryGetValue(itemInstance.ItemDefinition, out int currentCount))
            {
                ItemsCountByDefinition[itemInstance.ItemDefinition] = ++currentCount;
            }
            else
            {
                ItemsCountByDefinition[itemInstance.ItemDefinition] = 1;
                ItemsList.Add(itemInstance.ItemDefinition);
            }
        }
    }
}
