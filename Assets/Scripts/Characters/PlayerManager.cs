using UnityEngine;

public class PlayerManager : CharacterManager
{
    // private Apparel _equippedHead;
    // private Apparel _equippedTorso;
    void Start() {
        // PlayerEventManager.Instance.itemEquipStatusChanged += OnEquipmentChanged;
        PlayerEventManager.Instance.itemUsed += OnItemUsed;
    }
    public override void TakeDamage(int damageTaken, BodyPart bodyPart) {
        int damageReduced = ArmorManager.Instance.GetDefense(bodyPart);
        damageTaken = Mathf.Max(damageTaken - damageReduced, 0);
        base.TakeDamage(damageTaken, bodyPart);
    }
    // private void OnEquipmentChanged(Item item, bool status) {
    //     if (item is Apparel apparelItem) {
    //         if (status) {
    //             EquipArmor(apparelItem);
    //         } else {
    //             UnequipArmor(apparelItem);
    //         }
    //     }
    // }
    // private void EquipArmor(Apparel armor) {
    //     if (armor.bodyPart == BodyPart.Head) {
    //         _equippedHead = armor;
    //     } else if (armor.bodyPart == BodyPart.Torso) {
    //         _equippedTorso = armor;
    //     }
    // }
    // private void UnequipArmor(Apparel armor) {
    //     if (_equippedHead == armor) {
    //         _equippedHead = null;
    //     } else if (_equippedTorso == armor) {
    //         _equippedTorso = null;
    //     }
    // }
    private void OnItemUsed(Item item) {
        if (item is Medical medicalItem) {
            HP = Mathf.Min(HP + medicalItem.healing, _maxHp);
        }
    }

    protected override void OnDeath() {
        Debug.Log("Game over");
        base.OnDeath();
    }

    void OnDestroy() {
        // PlayerEventManager.Instance.itemEquipStatusChanged -= OnEquipmentChanged;
        PlayerEventManager.Instance.itemUsed -= OnItemUsed;
    }

}
