using UnityEngine;
using UnityEngine.UI;

public class SelectedButtonScript : MonoBehaviour
{
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Color highlightedColor = Color.green;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private WeaponScript weaponScript;

    private void OnWeaponChanged(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Gun) {
            SetButtonHighlight(button2, false);
            SetButtonHighlight(button1, true);
        } else if (weaponType == WeaponType.Rifle) {
            SetButtonHighlight(button1, false);
            SetButtonHighlight(button2, true);
        }
    }

    private void SetButtonHighlight(Button button, bool highlight)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = highlight ? highlightedColor : normalColor;
        }
    }

    void OnEnable() {
        weaponScript.weaponChanged += OnWeaponChanged;
    }    

    void OnDisable() {
        weaponScript.weaponChanged -= OnWeaponChanged;
    }
}
