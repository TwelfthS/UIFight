using UnityEngine;

public class PopupOpener : MonoBehaviour
{
    private ItemController _currentItem;
    void Awake() {
        _currentItem = GetComponent<ItemController>();
    }
    public void OpenPopup() {
        PopupManager.Instance.OpenPopup(_currentItem);
    }
    public void ClosePopup() {
        PopupManager.Instance.ClosePopup();
    }
}
