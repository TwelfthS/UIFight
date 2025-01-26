using UnityEngine;

public class PlayerManager : CharacterManager
{
    private int totalDefense;
    private int equippedWeaponDamage;

    public override void TakeDamage(int damageTaken) {
        damageTaken = Mathf.Max(damageTaken - totalDefense, 0);
        base.TakeDamage(damageTaken);
    }

    public override void DealDamage() {
        opponent.TakeDamage(damage + equippedWeaponDamage);
    }

    public void EquipWeapon(int weaponDamage) {
        equippedWeaponDamage = weaponDamage;
    }
}
