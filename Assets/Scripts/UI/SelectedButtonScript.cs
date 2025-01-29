using UnityEngine;
using UnityEngine.UI;

public class SelectedButtonScript : MonoBehaviour
{
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    [SerializeField] private Color _highlightedColor = Color.green;
    [SerializeField] private Color _normalColor = Color.white;

    private void OnWeaponChanged(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Gun) {
            SetButtonHighlight(_button2, false);
            SetButtonHighlight(_button1, true);
        } else if (weaponType == WeaponType.Rifle) {
            SetButtonHighlight(_button1, false);
            SetButtonHighlight(_button2, true);
        }
    }

    private void SetButtonHighlight(Button button, bool highlight)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = highlight ? _highlightedColor : _normalColor;
        }
    }

    void OnEnable() {
        WeaponManager.Instance.weaponChanged += OnWeaponChanged;
    }    

    void OnDisable() {
        WeaponManager.Instance.weaponChanged -= OnWeaponChanged;
    }
}
