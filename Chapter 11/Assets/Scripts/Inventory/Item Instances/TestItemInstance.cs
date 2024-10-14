namespace InventorySystem
{
    public class TestItemInstance :ItemInstance
    {
        public TestItemInstance(ItemDefinition itemDefinition) :base(itemDefinition)
        {
        }

        public override bool ConsumeItem(ItemConsumptionContext itemConsumptionContext)
        {
            return true;
        }
    }
}
