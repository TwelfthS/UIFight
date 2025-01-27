using UnityEngine;

public class PlayerManager : CharacterManager
{
    private int totalDefense;
    private int equippedWeaponDamage;

    public override void TakeDamage(int damageTaken) {
        damageTaken = Mathf.Max(damageTaken - totalDefense, 0);
        base.TakeDamage(damageTaken);
    }

    public override void DealDamage(int damage) {
        opponent.TakeDamage(damage + equippedWeaponDamage);
    }
    private void OnEquipmentChanged(Item item, bool status) {
        if (item is Apparel apparelItem) {
            if (status) {
                EquipArmor(apparelItem.defense);
            } else {
                UnequipArmor(apparelItem.defense);
            }
        }
    }
    private void EquipWeapon(int weaponDamage) {
        equippedWeaponDamage = weaponDamage;
    }
    private void EquipArmor(int armorDefense) {
        totalDefense += armorDefense;
    }
    private void UnequipArmor(int armorDefense) {
        totalDefense = Mathf.Max(totalDefense - armorDefense, 0);
    }
    private void OnItemUsed(Item item) {
        if (item is Medical medicalItem) {
            HP = Mathf.Min(HP + medicalItem.healing, maxHp);
        }
    }
    void OnEnable() {
        PlayerEventManager.Instance.itemEquipStatusChanged += OnEquipmentChanged;
        PlayerEventManager.Instance.itemUsed += OnItemUsed;
    }
    void OnDisable() {
        PlayerEventManager.Instance.itemEquipStatusChanged -= OnEquipmentChanged;
        PlayerEventManager.Instance.itemUsed -= OnItemUsed;
    }

}
