using UnityEngine;

public class ArmorSlot : InventorySlot
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private Apparel.BodyPart armorBodyPart;

    protected override void OnDropItem(ItemController itemController) {
        if (itemController.item is Apparel apparelItem && apparelItem.bodyPart == armorBodyPart) {
            if (content != null) {
                ItemController storedItem = transform.GetChild(0).gameObject.GetComponent<ItemController>();
                if (storedItem != null) {
                    storedItem.transform.SetParent(itemController.parentAfterDrag);
                }
            }
            base.OnDropItem(itemController);
            PlayerEventManager.Instance.InvokeItemEquipStatusChanged(itemController.item, true);
        }
    }

    protected override void OnItemLeft() {
        PlayerEventManager.Instance.InvokeItemEquipStatusChanged(content.item, false);
        base.OnItemLeft();
    }
}
