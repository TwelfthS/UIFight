using UnityEngine;
using System;

public class WeaponScript : MonoBehaviour
{
    public Action<WeaponType> weaponChanged;
    private int gunDamage = 5;
    private int rifleDamage = 9;
    private int gunAmmoUsage = 1;
    private int autoAmmoUsage = 3;
    private PlayerManager player;
    private WeaponType activeWeapon = WeaponType.Gun;
    void Awake() {
        player = GetComponent<PlayerManager>();
    }
    void Start() {
        weaponChanged?.Invoke(activeWeapon);
    }
    public void Shoot() {
        switch (activeWeapon) {
            case WeaponType.Gun:
                if (CheckForAmmo(AmmoType.Gun)) {
                    player.DealDamage(gunDamage);
                }
                break;
            case WeaponType.Rifle:
                if (CheckForAmmo(AmmoType.Auto)) {
                    player.DealDamage(rifleDamage);
                }
                break;
            default:
                break;
        }
    }
    
    public void EquipGun() {
        activeWeapon = WeaponType.Gun;
        weaponChanged?.Invoke(activeWeapon);
    }
    public void EquipRifle() {
        activeWeapon = WeaponType.Rifle;
        weaponChanged?.Invoke(activeWeapon);
    }

    private bool CheckForAmmo(AmmoType ammoType) {
        foreach (InventorySlot slot in InventoryManager.Instance.slots) {
            if (slot.content != null && slot.content.item is Ammo ammo && ammo.ammoType == ammoType) {
                if (ammoType == AmmoType.Auto && slot.content.Count >= 3) {
                    UseAmmo(slot.content, autoAmmoUsage);
                    return true;
                } else if (ammoType == AmmoType.Gun) {
                    UseAmmo(slot.content, gunAmmoUsage);
                    return true;
                }
            }
        }
        return false;
    }

    private void UseAmmo(ItemController itemController, int toUse) {
        itemController.ReduceCount(toUse);
    }
}
