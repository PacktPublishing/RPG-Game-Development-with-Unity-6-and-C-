using InventorySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "TestItemDefinition", menuName = "Scriptable Objects/TestItemDefinition")]
public class TestItemDefinition : ItemDefinition
{
    public override ItemInstance MakeInstance()
    {
        return new TestItemInstance(this);
    }
}
