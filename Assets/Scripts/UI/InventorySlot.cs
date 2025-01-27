using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public ItemController content;
    public void OnDrop(PointerEventData eventData) {
        ItemController itemController = eventData.pointerDrag.GetComponent<ItemController>();
        OnDropItem(itemController);
    }

    protected virtual void OnDropItem(ItemController itemController) {
        if (content == null) {
            itemController.parentAfterDrag.gameObject.GetComponent<InventorySlot>().OnItemLeft();
            itemController.parentAfterDrag = transform;
            content = itemController;
        }
    }

    protected virtual void OnItemLeft() {
        content = null;
    }
}
