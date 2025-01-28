using UnityEngine;

public class PlayerManager : CharacterManager
{
    private Apparel equippedHead;
    private Apparel equippedTorso;
    void Start() {
        PlayerEventManager.Instance.itemEquipStatusChanged += OnEquipmentChanged;
        PlayerEventManager.Instance.itemUsed += OnItemUsed;
    }
    public override void TakeDamage(int damageTaken) {
        damageTaken = Mathf.Max(damageTaken - (equippedHead ? equippedHead.defense : 0) - (equippedTorso ? equippedTorso.defense : 0), 0);
        base.TakeDamage(damageTaken);
    }

    public override void DealDamage(int damage) {
        opponent.TakeDamage(damage);
    }
    private void OnEquipmentChanged(Item item, bool status) {
        if (item is Apparel apparelItem) {
            if (status) {
                EquipArmor(apparelItem);
            } else {
                UnequipArmor(apparelItem);
            }
        }
    }
    private void EquipArmor(Apparel armor) {
        if (armor.bodyPart == BodyPart.Head) {
            equippedHead = armor;
        } else if (armor.bodyPart == BodyPart.Torso) {
            equippedTorso = armor;
        }
    }
    private void UnequipArmor(Apparel armor) {
        if (equippedHead == armor) {
            equippedHead = null;
        } else if (equippedTorso == armor) {
            equippedTorso = null;
        }
    }
    private void OnItemUsed(Item item) {
        if (item is Medical medicalItem) {
            HP = Mathf.Min(HP + medicalItem.healing, maxHp);
        }
    }

    void OnDestroy() {
        PlayerEventManager.Instance.itemEquipStatusChanged -= OnEquipmentChanged;
        PlayerEventManager.Instance.itemUsed -= OnItemUsed;
    }

}
