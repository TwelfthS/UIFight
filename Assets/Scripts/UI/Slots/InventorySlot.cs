using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public ItemController content;
    public void OnDrop(PointerEventData eventData) {
        ItemController itemController = eventData.pointerDrag.GetComponent<ItemController>();
        OnDropItem(itemController);
    }

    public virtual void OnItemLeft() {
        content.goneFromSlot -= OnItemLeft;
        content = null;
    }

    public void SetContent(ItemController itemController) {
        content = itemController;
        content.transform.SetParent(transform);
        content.goneFromSlot += OnItemLeft;
    }

    public void SwapContent(InventorySlot other) {
        if (other.content == null || content == null) {
            Debug.Log("nothing to swap");
            return;
        }
        ItemController tempContent1 = content;
        ItemController tempContent2 = other.content;
        content.InvokeGoneFromSlot();
        other.content.InvokeGoneFromSlot();
        SetContent(tempContent2);
        other.SetContent(tempContent1);
    }

    public void CleanUp() {
        foreach (Transform child in transform) {
            if (child.gameObject.GetComponent<ItemController>() != null) {
                if (content != null) content.goneFromSlot -= OnItemLeft;
                content = null;
                Destroy(child.gameObject);
            }
        }
    }

    protected virtual void OnDropItem(ItemController itemController) {
        if (content == null) {
            if (itemController.parentAfterDrag != transform) {
                itemController.InvokeGoneFromSlot();
            }
            itemController.parentAfterDrag = transform;
            SetContent(itemController);
        }
    }
    public void InitSlot() {
        foreach (Transform child in transform) {
            ItemController childItem = child.GetComponent<ItemController>();
            if (childItem != null) {
                SetContent(childItem);
                return;
            }
        }
    }
}
