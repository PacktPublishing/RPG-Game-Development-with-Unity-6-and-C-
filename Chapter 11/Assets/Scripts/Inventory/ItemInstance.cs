namespace InventorySystem
{
    public abstract class ItemInstance
    {
        private static int idAcc;
        public int ItemId;
        public ItemDefinition ItemDefinition;

        public ItemInstance(ItemDefinition itemDefinition)
        {
            ItemId = idAcc++;
            ItemDefinition = itemDefinition;
        }

        public abstract bool ConsumeItem(ItemConsumptionContext itemConsumptionContext);
    }
}
