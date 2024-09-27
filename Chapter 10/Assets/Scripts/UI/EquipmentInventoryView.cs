using UnityEngine;
using UnityEngine.UIElements;
using InventorySystem;

public class EquipmentInventoryView : MonoBehaviour
{
    public ItemInstance CurrentItemInstance
    {
        get;
        set;
    }

    private Image _mainImage;
    private Label _mainCountLabel;
    private Image _prevImage;
    private Label _prevCountLabel;
    private Image _nextImage;
    private Label _nextCountLabel;

    private int _currentIndex;
    private Inventory _inventory;

    private void OnEnable()
    {
        _mainImage = GetComponent<UIDocument>().rootVisualElement.Q<Image>("MainImage");
        _prevImage = GetComponent<UIDocument>().rootVisualElement.Q<Image>("PrevImage");
        _nextImage = GetComponent<UIDocument>().rootVisualElement.Q<Image>("NextImage");
        _mainCountLabel = GetComponent<UIDocument>().rootVisualElement.Q<Label>("MainCount");
        _prevCountLabel = GetComponent<UIDocument>().rootVisualElement.Q<Label>("PrevCount");
        _nextCountLabel = GetComponent<UIDocument>().rootVisualElement.Q<Label>("NextCount");
    }

    public void LoadInventory(Inventory inventory)
    {
        if (_inventory != null)
        {
            _inventory.ItemsUpdated -= UpdateView;
        }

        _inventory = inventory;
        if (_inventory.ItemsList.Count >= 3)
        {
            _currentIndex = 1;
        }
        else
        {
            _currentIndex = 0;
        }

        UpdateView();
        _inventory.ItemsUpdated += UpdateView;
    }

    public void ShowNextItem()
    {
        if (_currentIndex == _inventory.ItemsList.Count - 1)
        {
            _currentIndex = 0;
        }
        else
        {
            _currentIndex++;
        }

        UpdateView();
    }

    public void ShowPreviousItem()
    {
        if(_currentIndex == 0)
        {
            _currentIndex = _inventory.ItemsList.Count - 1;
        }
        else
        {
            _currentIndex--;
        }

        UpdateView();
    }

    private void UpdateView()
    {
        SetSprite(_mainImage, _mainCountLabel, _currentIndex);
        SetSprite(_prevImage, _prevCountLabel, _currentIndex - 1);
        SetSprite(_nextImage, _nextCountLabel, _currentIndex + 1);

        if (_currentIndex < _inventory.ItemsList.Count && _currentIndex >= 0)
        {
            CurrentItemInstance = _inventory.GetItemOfTypeDefinition(_inventory.ItemsList[_currentIndex]);
        }
        else
        {
            CurrentItemInstance = null;
        }
    }

    private void SetSprite(Image targetImage, Label targetLabel, int index)
    {
        if(index >= _inventory.ItemsList.Count || index < 0)
        {
            targetImage.image = null;
            targetLabel.text = string.Empty;
            return;
        }

        targetImage.image = _inventory.ItemsList[index].Icon;
        targetLabel.text = _inventory.ItemsCountByDefinition[_inventory.ItemsList[index]].ToString();

    }
}
