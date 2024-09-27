using Player;
using InventorySystem;
using UnityEngine;

public class PickableItemInstance : MonoBehaviour
{
    [SerializeField]
    private ItemDefinition _itemDefinition;
    private TagHandle _tagHandle;

    void Start()
    {
        _tagHandle = TagHandle.GetExistingTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_tagHandle))
        {
            return;
        }

        ItemInstance item = _itemDefinition.MakeInstance();
        other.GetComponentInParent<PlayerInventoryController>().AddItem(item);
        Destroy(gameObject);
    }
}
