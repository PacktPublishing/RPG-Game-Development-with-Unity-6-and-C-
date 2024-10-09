using InventorySystem;
using System;
using UnityEngine.UIElements;

public class InventoryListEntryController
{
    ItemDefinition _itemDefinition;
    Inventory _targetInventory;

    Button _actionButton;
    Label _label;
    Label _itemPrice;
    Label _itemAvailable;
    Image _image;

    ~InventoryListEntryController()
    {
        if(_targetInventory != null)
        {
            _targetInventory.ItemsUpdated -= OnItemsUpdated;
        }
    }

    // This function retrieves a reference to the 
    // character name label inside the UI element.
    public void SetVisualElement(VisualElement visualElement)
    {
        _label = visualElement.Q<Label>("ItemName");
        _image = visualElement.Q<Image>("ItemIcon");
        _actionButton = visualElement.Q<Button>("ActionButton");
        _itemPrice = visualElement.Q<Label>("Price");
        _itemAvailable = visualElement.Q<Label>("Available");
    }

    // This function receives the character whose name this list 
    // element is supposed to display. Since the elements list 
    // in a `ListView` are pooled and reused, it's necessary to 
    // have a `Set` function to change which character's data to display.
    public void SetItemData(ItemDefinition itemDefinition, Action<ItemDefinition> itemAction, string buttonText, int price, Inventory targetInventory)
    {
        _itemDefinition = itemDefinition;
        _label.text = itemDefinition.Name;
        _image.image = itemDefinition.Icon;
        _actionButton.clicked += () =>
        {
            itemAction(itemDefinition);
        };
        _actionButton.text = buttonText;
        _itemPrice.text = price.ToString();
        targetInventory.ItemsUpdated += OnItemsUpdated;
        _targetInventory = targetInventory;
        OnItemsUpdated();
    }

    private void OnItemsUpdated()
    {
        if (!_targetInventory.ItemsCountByDefinition.ContainsKey(_itemDefinition))
        {
            _itemAvailable.text = "0";
            return;
        }

        _itemAvailable.text = _targetInventory.ItemsCountByDefinition[_itemDefinition].ToString();
    }
}
