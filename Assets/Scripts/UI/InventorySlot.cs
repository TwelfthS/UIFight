using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public ItemController content;
    void Start() {
        if (transform.childCount > 0) {
            content = transform.GetChild(0).GetComponent<ItemController>();
        }
    }
        
    public void OnDrop(PointerEventData eventData) {
        ItemController itemController = eventData.pointerDrag.GetComponent<ItemController>();
        OnDropItem(itemController);
    }

    protected virtual void OnDropItem(ItemController itemController) {
        if (content == null) {
            SetContent(itemController);
        }
    }

    public virtual void OnItemLeft() {
        content = null;
    }

    public void SetContent(ItemController itemController) {
        itemController.parentSlot.OnItemLeft();
        itemController.parentSlot = this;
        itemController.MoveToParent();
        content = itemController;
    }
}
