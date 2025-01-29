using UnityEngine;
using System;

public class ArmorSlot : InventorySlot
{
    [field: SerializeField] public BodyPart armorBodyPart { get; private set; }
    public event Action<BodyPart> contentChanged;
    protected override void OnDropItem(ItemController itemController) {
        if (itemController.item is Apparel apparelItem && apparelItem.bodyPart == armorBodyPart) {
            PutToSlot(itemController);
        }
    }

    public override void OnItemLeft() {
        base.OnItemLeft();
        contentChanged?.Invoke(armorBodyPart);
    }

    public void PutToSlot(ItemController itemController) {
        if (content != null) {
            Transform itemParent = itemController.transform.parent;
            InventorySlot oldParent = itemParent == transform.root ? itemController.parentAfterDrag.GetComponent<InventorySlot>() : itemParent.GetComponent<InventorySlot>();
            SwapContent(oldParent);
        } else {
            itemController.InvokeGoneFromSlot();
            SetContent(itemController);
        }
        itemController.parentAfterDrag = transform;
        if (content.item is Apparel apparel) {
            contentChanged?.Invoke(armorBodyPart);
        }
    }
}
