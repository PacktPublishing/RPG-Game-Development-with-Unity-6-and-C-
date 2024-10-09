using InventorySystem;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Player;
using System.Linq;

public class StoreUIView : MonoBehaviour
{
    public static event Action StoreOpened, StoreClosed;

    [SerializeField]
    private VisualTreeAsset _listEntryTemplate;

    private PlayerInventoryController _playerInventoryController;
    private StoreCharacterController _storeCharacterController;
    private Label _buyingInventoryCreditLabel;
    private Label _sellingInventoryCreditLabel;
    private ListView _sellingInventoryRoot;

    public void Initialize(PlayerInventoryController playerInventoryController, StoreCharacterController storeCharacterController)
    {
        _playerInventoryController = playerInventoryController;
        _storeCharacterController = storeCharacterController;
        StoreOpened?.Invoke();
    }

    public void OnClosePressed()
    {
        gameObject.SetActive(false);
        StoreClosed?.Invoke();
    }

    private void OnEnable()
    {
        ListView buyingInventoryRoot = GetComponent<UIDocument>().rootVisualElement.Q<TemplateContainer>("BuyingInventory").Q<ListView>("InventoryView");
        ListView sellingInventoryRoot = GetComponent<UIDocument>().rootVisualElement.Q<TemplateContainer>("SellingInventory").Q<ListView>("InventoryView");
        GetComponent<UIDocument>().rootVisualElement.Q<Button>("CloseButton").clicked += OnClosePressed;

        InitializeInventoryWithListView(buyingInventoryRoot, true);
        InitializeInventoryWithListView(sellingInventoryRoot, false);

        _buyingInventoryCreditLabel = GetComponent<UIDocument>().rootVisualElement.Q<TemplateContainer>("BuyingInventory").Q<Label>("CreditLabel");
        _sellingInventoryCreditLabel = GetComponent<UIDocument>().rootVisualElement.Q<TemplateContainer>("SellingInventory").Q<Label>("CreditLabel");
        _playerInventoryController.Inventory.ItemsUpdated += OnBuyingInventoryChanged;
        _storeCharacterController.Inventory.ItemsUpdated += OnSellingInventoryChanged;

        OnBuyingInventoryChanged();
        OnSellingInventoryChanged();
    }

    private void OnDisable()
    {
        _storeCharacterController.Inventory.ItemsUpdated -= OnBuyingInventoryChanged;
        _playerInventoryController.Inventory.ItemsUpdated -= OnSellingInventoryChanged;
    }

    private void OnBuyingInventoryChanged()
    {
        _buyingInventoryCreditLabel.text = _playerInventoryController.Inventory.Credits.ToString();
    }

    private void OnSellingInventoryChanged()
    {
        _sellingInventoryCreditLabel.text = _storeCharacterController.Inventory.Credits.ToString();
    }

    private void InitializeInventoryWithListView(ListView root, bool isBuying)
    {
        Inventory targetInventory = isBuying ? _playerInventoryController.Inventory : _storeCharacterController.Inventory;
        List <ItemDefinition> targetList = isBuying ? _storeCharacterController.ItemsToSell.Keys.ToList() : _storeCharacterController.ItemsToBuy.Keys.ToList();
        root.makeItem += CreateInventoryItem;
        // Set up bind function for a specific list entry
        root.bindItem = (item, index) =>
        {
            (item.userData as InventoryListEntryController)?.SetItemData(targetList[index], 
                                                                         isBuying ? _storeCharacterController.Sell : _storeCharacterController.Buy,
                                                                         isBuying ? "Sell" : "Buy",
                                                                         isBuying ? _storeCharacterController.ItemsToSell[targetList[index]] : _storeCharacterController.ItemsToBuy[targetList[index]],
                                                                         targetInventory);
        };

        root.fixedItemHeight = 90;
        root.itemsSource = targetList;
    }

    private VisualElement CreateInventoryItem()
    {
        // Instantiate the UXML template for the entry
        var newListEntry = _listEntryTemplate.Instantiate();

        // Instantiate a controller for the data
        var newListEntryLogic = new InventoryListEntryController();

        // Assign the controller script to the visual element
        newListEntry.userData = newListEntryLogic;

        // Initialize the controller script
        newListEntryLogic.SetVisualElement(newListEntry);

        return newListEntry;
    }
}