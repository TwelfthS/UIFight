using UnityEngine;

public class PopupOpener : MonoBehaviour
{
    private ItemController currentItem;
    void Awake() {
        currentItem = GetComponent<ItemController>();
    }
    public void OpenPopup() {
        PopupManager.Instance.OpenPopup(currentItem);
    }
    public void ClosePopup() {
        PopupManager.Instance.ClosePopup();
    }
}
