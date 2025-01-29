using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemText;
    [SerializeField] private TMP_Text _itemStat;
    [SerializeField] private TMP_Text _itemWeight;
    [SerializeField] private Button _useButton;
    [SerializeField] private TMP_Text _useButtonText;
    [SerializeField] private Image _itemIcon;
    private ItemController _owner;

    public void InitPopup(ItemController owner) {
        _owner = owner;
        if (_owner) {
            _itemText.text = _owner.item.itemName;
            _itemIcon.sprite = _owner.item.icon;
            _itemWeight.text = _owner.item.weight.ToString() + " кг";
            _useButton.onClick.AddListener(_owner.UseItem);
            SetTypeSpecificText(_owner.item);
        }
    }

    public void SetTypeSpecificText(Item item) {
        string buttonText = "";
        string statText = "";
        switch (item) {
            case Medical medical:
                buttonText = "Лечить";
                statText = "+" + medical.healing + "hp";
                break;
            case Ammo ammo:
                buttonText = "Купить";
                break;
            case Apparel apparel:
                if (ArmorManager.Instance.IsEquipped(apparel)) {
                    buttonText = "Снять";
                } else {
                    buttonText = "Экипировать";
                }
                statText = "+" + apparel.defense;
                break;
            default:
                buttonText = "Error";
                break;
        }
        _useButtonText.text = buttonText;
        _itemStat.text = statText;
    }

    public void DeleteItem() {
        if (_owner) {
            Destroy(_owner.gameObject);            
        }
    }

    void OnDisable() {
        if (_owner) {
            _useButton.onClick.RemoveListener(_owner.UseItem);            
        }
    }

}
