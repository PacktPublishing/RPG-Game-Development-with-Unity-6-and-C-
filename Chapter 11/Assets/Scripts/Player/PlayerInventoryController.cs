using BasicFiniteStateMachine;
using InventorySystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInventoryController : MonoBehaviour
    {
        public Inventory Inventory => _inventory;

        public Vector3 TargetPosition { get; set; }
        [SerializeField]
        private LifeController _lifeController;
        [SerializeField]
        private InventoryDefinition _inventoryDefinition;
        [SerializeField]
        private EquipmentInventoryView _euipmentInventoryView;

        [SerializeField]
        private InputActionReference _consumeInputAction;
        [SerializeField]
        private InputActionReference _nextInputAction;
        [SerializeField]
        private InputActionReference _previousInputAction;

        private Inventory _inventory;

        public void AddItem(ItemInstance itemInstance)
        {
            _inventory.AddItem(itemInstance);
        }

        private void Start()
        {
            _inventory = new(_inventoryDefinition);
            _euipmentInventoryView.LoadInventory(_inventory);
            _consumeInputAction.action.performed += OnConsumePressed;
            _nextInputAction.action.performed += OnInventoryNextPressed;
            _previousInputAction.action.performed += OnInventoryNextPressed;
        }

        private void OnConsumePressed(InputAction.CallbackContext callbackContext)
        {
            if(_euipmentInventoryView.CurrentItemInstance == null)
            {
                return;
            }

            ItemConsumptionContext itemConsumptionContext = new();
            itemConsumptionContext.HealthHandler = _lifeController;
            itemConsumptionContext.ActivationPosition = TargetPosition;

            if(_euipmentInventoryView.CurrentItemInstance.ConsumeItem(itemConsumptionContext))
            {
                _inventory.RemoveItem(_euipmentInventoryView.CurrentItemInstance.ItemId);
            }
        }

        private void OnInventoryNextPressed(InputAction.CallbackContext callbackContext)
        {
            _euipmentInventoryView.ShowNextItem();
        }

        private void OnInventoryPreviousPressed(InputAction.CallbackContext callbackContext)
        {
            _euipmentInventoryView.ShowPreviousItem();
        }
    }
}