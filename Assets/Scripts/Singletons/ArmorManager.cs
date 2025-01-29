using UnityEngine;

public class ArmorManager : MonoBehaviour
{
    public static ArmorManager Instance { get; private set; }
    [SerializeField] public ArmorSlot headSlot;
    [SerializeField] public ArmorSlot torsoSlot;
    private int numberOfSlots = 2;
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
        if (headSlot.armorBodyPart != BodyPart.Head || torsoSlot.armorBodyPart != BodyPart.Torso) {
            Debug.Log("Armor slots are assigned incorrectly, equip button will not work properly");
        }
    }

    public void EquipArmor(ItemController itemController) {
        if (itemController.item is Apparel armor) {
            ArmorSlot slotByBodyPart = GetSlotByBodyPart(armor.bodyPart);
            if (slotByBodyPart) {
                slotByBodyPart.PutToSlot(itemController);
            }
        }
    }

    public void UnquipArmor(BodyPart bodyPart) {
        ArmorSlot slotByBodyPart = GetSlotByBodyPart(bodyPart);
        if (slotByBodyPart && slotByBodyPart.content) {
            InventorySlot freeSlot = InventoryManager.Instance.GetFirstFreeSlot();
            if (freeSlot) {
                ItemController content = slotByBodyPart.content;
                slotByBodyPart.content.InvokeGoneFromSlot();
                freeSlot.SetContent(content);
            }
        }
    }

    public ArmorSlot GetSlotByBodyPart(BodyPart bodyPart) {
        ArmorSlot result = null;
        if (bodyPart == BodyPart.Head) {
            result = headSlot;
        } else if (bodyPart == BodyPart.Torso) {
            result = torsoSlot;
        }
        return result;
    }

    public int GetDefense(BodyPart bodyPart) {
        Apparel armor = GetApparel(bodyPart);
        if (armor != null) {
            return armor.defense;
        } else {
            return 0;
        }
    }

    public Apparel GetApparel(BodyPart bodyPart) {
        if (bodyPart == BodyPart.Head) {
            if (headSlot.content != null && headSlot.content.item is Apparel apparel) {
                return apparel;
            }
        } else if (bodyPart == BodyPart.Torso) {
            if (torsoSlot.content != null && torsoSlot.content.item is Apparel apparel) {
                return apparel;
            }
        }
        return null;
    }

    public ItemData[] GetEquippedArmor() {
        ItemData[] data = new ItemData[numberOfSlots];
        data[0] = InventoryManager.Instance.ItemControllerToData(headSlot.content);
        data[1] = InventoryManager.Instance.ItemControllerToData(torsoSlot.content);
        return data;
    }

    public void LoadArmor(ItemData[] itemData) {
        foreach (ItemData item in itemData) {
            ItemController createdItem = GameManager.Instance.LoadItem(item);
            if (createdItem) EquipArmor(createdItem);
        }
    }

    public bool IsEquipped(Apparel apparel) {
        ArmorSlot slotByBodyPart = GetSlotByBodyPart(apparel.bodyPart);
        if (slotByBodyPart && slotByBodyPart.content && slotByBodyPart.content.item is Apparel storedApparel && storedApparel == apparel) {
            return true;
        } else {
            return false;
        }
    }
}
