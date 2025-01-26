using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private int gunDamage = 5;
    private int rifleDamage = 9;
    private PlayerManager player;
    void Start() {
        player = GetComponent<PlayerManager>();
    }
    public void EquipGun() {
        player.EquipWeapon(gunDamage);
    }
    public void EquipRifle() {
        player.EquipWeapon(rifleDamage);
    }
}
