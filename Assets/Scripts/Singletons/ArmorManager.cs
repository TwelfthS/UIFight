using UnityEngine;

public class ArmorManager : MonoBehaviour
{
    public static ArmorManager Instance { get; private set; }
    [SerializeField] public ArmorSlot headSlot;
    [SerializeField] public ArmorSlot torsoSlot;
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
            if (armor.bodyPart == BodyPart.Head) {
                headSlot.PutToSlot(itemController);
            } else if (armor.bodyPart == BodyPart.Torso) {
                torsoSlot.PutToSlot(itemController);
            }
        }
    }
}
