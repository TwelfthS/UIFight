using UnityEngine;
using UnityEngine.UI;

public class SelectedButtonScript : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Color highlightedColor = Color.green;
    public Color normalColor = Color.white;

    private Button currentSelectedButton;

    void Start()
    {
        button1.onClick.AddListener(() => OnWeaponButtonClicked(button1));
        button2.onClick.AddListener(() => OnWeaponButtonClicked(button2));
        // OnWeaponButtonClicked(weaponButton1);
    }

    void OnWeaponButtonClicked(Button clickedButton)
    {
        if (currentSelectedButton != null && currentSelectedButton != clickedButton)
        {
            SetButtonHighlight(currentSelectedButton, false);
        }
        SetButtonHighlight(clickedButton, true);
        currentSelectedButton = clickedButton;
    }

    void SetButtonHighlight(Button button, bool highlight)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = highlight ? highlightedColor : normalColor;
        }
    }
}
