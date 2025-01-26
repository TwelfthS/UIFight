using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set;}
    public GameObject popup;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void OpenPopup() {
        popup.SetActive(true);
    }
    public void ClosePopup() {
        popup.SetActive(false);
    }
}
