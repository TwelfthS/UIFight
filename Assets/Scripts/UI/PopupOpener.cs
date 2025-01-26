using UnityEngine;

public class PopupOpener : MonoBehaviour
{
    public void OpenPopup() {
        PopupManager.Instance.OpenPopup();
    }
    public void ClosePopup() {
        PopupManager.Instance.ClosePopup();
    }
}
