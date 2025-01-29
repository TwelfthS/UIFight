using UnityEngine;
using System;

public class ArmorSlot : InventorySlot
{
    [field: SerializeField] public BodyPart armorBodyPart { get; private set; }
    public event Action<BodyPart> contentChanged;
    // protected override void OnDropItem(ItemController itemController) {
    //     if (itemController.item is Apparel apparelItem && apparelItem.bodyPart == armorBodyPart) {
    //         PutToSlot(itemController);
    //     }
    // }

    public override void OnItemLeft() {
        contentChanged?.Invoke(armorBodyPart);
        base.OnItemLeft();
    }

    public void PutToSlot(ItemController itemController) {
        if (content != null) {
            InventorySlot oldParent = itemController.transform.parent.GetComponent<InventorySlot>();
            SwapContent(oldParent);
        } else {
            itemController.InvokeGoneFromSlot();
            SetContent(itemController);
        }
        if (content.item is Apparel apparel) {
            contentChanged?.Invoke(armorBodyPart);
        }
    }
}
