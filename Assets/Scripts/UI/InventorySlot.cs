using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0) {
            ItemController itemController = eventData.pointerDrag.GetComponent<ItemController>();
            itemController.parentAfterDrag = transform;
        }
    }
}
