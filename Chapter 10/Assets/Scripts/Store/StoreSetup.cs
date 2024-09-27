using InventorySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreSetup", menuName = "Scriptable Objects/StoreSetup")]
public class StoreSetup : ScriptableObject
{
    public InventoryDefinition InitialInventorySetup;
    public ItemDefinition[] ItemsToBuy;
    public int[] BuyPrice;
    public ItemDefinition[] ItemsToSell;
    public int[] SellPrice;
}
