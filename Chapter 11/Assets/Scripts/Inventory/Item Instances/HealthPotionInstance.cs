namespace InventorySystem
{
    public class HealthPotionInstance :ItemInstance
    {
        public int AddedHealth;

        public HealthPotionInstance(int addedHealth, ItemDefinition itemDefinition) :base(itemDefinition)
        {
            AddedHealth = addedHealth;
        }

        public override bool ConsumeItem(ItemConsumptionContext itemConsumptionContext)
        {
            return itemConsumptionContext.HealthHandler.AddHealth(AddedHealth);
        }
    }
}
