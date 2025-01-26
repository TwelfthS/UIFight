using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private CharacterManager owner;
    [SerializeField] private Image healthBarImage;
    void OnEnable() {
        if (owner != null) {
            owner.OnHPChanged += UpdateHealthBar;            
        }
    }
    void OnDisable() {
        if (owner != null) {
            owner.OnHPChanged -= UpdateHealthBar;            
        }
    }

    public void UpdateHealthBar(int hp, int maxHp) {
        if (healthBarImage != null) {
            healthBarImage.fillAmount = (float)hp / (float)maxHp;
        }
    }
}
