using Player;
using InventorySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoreCharacterController : MonoBehaviour
{
    public Dictionary<ItemDefinition, int> ItemsToBuy;
    public Dictionary<ItemDefinition, int> ItemsToSell;
    public Inventory Inventory => _inventory;
    [SerializeField] private StoreUIView _storeUIView;
    [SerializeField] private PlayerInventoryController _playerInventoryController;
    [SerializeField] private StoreSetup _storeSetup;

    [SerializeField] private GameObject _interactionCanvas;
    [SerializeField] private InputActionProperty _inputActionProperty;

    private Inventory _inventory;
    private TagHandle _tagHandle;
    private bool _isInside;

    private void Start()
    {
        _inventory = new(_storeSetup.InitialInventorySetup);
        _tagHandle = TagHandle.GetExistingTag("Player");
        ItemsToBuy = new();
        ItemsToSell = new();

        for(int i = 0; i< _storeSetup.ItemsToBuy.Length;i++)
        {
            ItemsToBuy[_storeSetup.ItemsToBuy[i]] = _storeSetup.BuyPrice[i];
        }

        for (int i = 0; i < _storeSetup.ItemsToSell.Length; i++)
        {
            ItemsToSell[_storeSetup.ItemsToSell[i]] = _storeSetup.SellPrice[i];
        }
    }

    public void Buy(ItemDefinition itemDefinition)
    {
        if(!ItemsToBuy.ContainsKey(itemDefinition))
        {
            return;
        }

        MakeTransaction(ItemsToBuy[itemDefinition], _playerInventoryController.Inventory, _inventory, itemDefinition);
    }

    public void Sell(ItemDefinition itemDefinition)
    {
        if (!ItemsToSell.ContainsKey(itemDefinition))
        {
            return;
        }

        MakeTransaction(ItemsToSell[itemDefinition], _inventory, _playerInventoryController.Inventory, itemDefinition);
    }

    public bool MakeTransaction(int price, Inventory buyingInventory, Inventory sellingInventory, ItemDefinition itemDefinition)
    {
        ItemInstance itemToBuy = sellingInventory.GetItemOfTypeDefinition(itemDefinition);
        if (itemToBuy == null)
        {
            return false;
        }

        if (buyingInventory.RemoveCredit(price))
        {
            buyingInventory.AddItem(itemToBuy);
            sellingInventory.AddCredit(price);
            sellingInventory.RemoveItem(itemToBuy.ItemId);
            return true;
        }

        return false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_tagHandle))
        {
            return;
        }

        _isInside = true;
        ToggleActivation(true);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_tagHandle))
        {
            return;
        }

        _isInside = false;
        ToggleActivation(false);
    }


    private void ToggleActivation(bool isOn)
    {
        _interactionCanvas.SetActive(isOn);
        if (isOn)
        {
            _inputActionProperty.action.performed += OnInteractionPerformed;
        }
        else
        {
            _inputActionProperty.action.performed -= OnInteractionPerformed;
        }
    }

    private void OnInteractionPerformed(InputAction.CallbackContext obj)
    {
        _storeUIView.Initialize(_playerInventoryController, this);
        _storeUIView.gameObject.SetActive(true);
    }
}
