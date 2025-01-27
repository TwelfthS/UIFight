using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private int gunDamage = 5;
    private int rifleDamage = 9;
    private PlayerManager player;
    private WeaponType activeWeapon = WeaponType.Gun;
    void Start() {
        player = GetComponent<PlayerManager>();
    }
    public void Shoot() {
        switch (activeWeapon) {
            case WeaponType.Gun:
                player.DealDamage(gunDamage);
                break;
            case WeaponType.Rifle:
                player.DealDamage(rifleDamage);
                break;
            default:
                break;
        }
    }
    
    public void EquipGun() {
        activeWeapon = WeaponType.Gun;
    }
    public void EquipRifle() {
        activeWeapon = WeaponType.Rifle;
    }

    public enum WeaponType {
        Gun, Rifle
    }
}
