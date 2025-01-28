using UnityEngine;

public class ArmorSlot : InventorySlot
{
    [field: SerializeField] public BodyPart armorBodyPart { get; private set; }    
    [SerializeField] private PlayerManager player;
    protected override void OnDropItem(ItemController itemController) {
        if (itemController.item is Apparel apparelItem && apparelItem.bodyPart == armorBodyPart) {
            PutToSlot(itemController);
        }
    }

    protected override void OnItemLeft() {
        PlayerEventManager.Instance.InvokeItemEquipStatusChanged(content.item, false);
        base.OnItemLeft();
    }

    public void PutToSlot(ItemController itemController) {
        if (content != null) {
            InventorySlot newSlot = itemController.parentSlot;
            newSlot.SetContent(content);
        }
        itemController.parentSlot = this;
        itemController.gameObject.transform.SetParent(itemController.parentSlot.transform);
        content = itemController;
        PlayerEventManager.Instance.InvokeItemEquipStatusChanged(itemController.item, true);
    }
}
