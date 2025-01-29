using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private CharacterManager _owner;
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private TMP_Text _hpText;
    void OnEnable() {
        if (_owner != null) {
            _owner.HPChanged += UpdateHealthBar;            
        }
    }
    void OnDisable() {
        if (_owner != null) {
            _owner.HPChanged -= UpdateHealthBar;            
        }
    }
    public void UpdateHealthBar(int hp, int maxHp) {
        if (_healthBarImage != null) {
            _healthBarImage.fillAmount = (float)hp / (float)maxHp;
            _hpText.text = hp.ToString();
        }
    }
}
