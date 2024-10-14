using System.Collections;
using InventorySystem;
using NUnit.Framework;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;

public class InventoryChangesTests
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestRemoveItem()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        // Arrange
        AsyncOperationHandle<InventoryDefinition>  opHandle = Addressables.LoadAssetAsync<InventoryDefinition>("Assets/ScriptableObjects/Inventory/TestingInventory.asset");
        AsyncOperationHandle<ItemDefinition>  opHandle2 = Addressables.LoadAssetAsync<ItemDefinition>("Assets/ScriptableObjects/Inventory/TestItem.asset");

        yield return opHandle;
        yield return opHandle2;

        Assert.AreEqual(opHandle.Status, AsyncOperationStatus.Succeeded); // Test that addressable is loaded successfully.
        InventoryDefinition testInventoryDefinition = opHandle.Result;
        ItemDefinition testItemDefinition = opHandle2.Result;

        Inventory inventoryA = new Inventory(testInventoryDefinition);

        // Act
        ItemInstance itemToRemove = inventoryA.GetItemOfTypeDefinition(testItemDefinition);
        inventoryA.RemoveItem(itemToRemove.ItemId);
        // Assert
        Assert.AreEqual(inventoryA.ItemsList.Count, 0);
        Assert.AreEqual(inventoryA.ItemsCountByDefinition.ContainsKey(testItemDefinition), false);
        Assert.AreEqual(inventoryA.GetItemOfTypeDefinition(testItemDefinition), null);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestAddItem()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        // Arrange
        AsyncOperationHandle<InventoryDefinition>  opHandle = Addressables.LoadAssetAsync<InventoryDefinition>("Assets/ScriptableObjects/Inventory/EmptyTestingInventory.asset");
        AsyncOperationHandle<ItemDefinition> opHandle2 = Addressables.LoadAssetAsync<ItemDefinition>("Assets/ScriptableObjects/Inventory/TestItem.asset");

        yield return opHandle;
        yield return opHandle2;

        Assert.AreEqual(opHandle.Status, AsyncOperationStatus.Succeeded); // Test that addressable is loaded successfully.
        InventoryDefinition testInventoryDefinition = opHandle.Result;
        ItemDefinition testItemDefinition = opHandle2.Result;

        Inventory inventoryB = new Inventory(testInventoryDefinition);

        // Act
        ItemInstance itemToAdd = testItemDefinition.MakeInstance();
        inventoryB.AddItem(itemToAdd);
        // Assert
        Assert.AreEqual(inventoryB.ItemsList.Count, 1);
        Assert.AreEqual(inventoryB.ItemsList[0], testItemDefinition);
        Assert.AreEqual(inventoryB.ItemsCountByDefinition[testItemDefinition], 1);
        Assert.AreEqual(inventoryB.GetItemOfTypeDefinition(testItemDefinition).ItemId, itemToAdd.ItemId);
    }
}
