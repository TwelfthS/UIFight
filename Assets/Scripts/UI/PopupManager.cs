using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set;}
    [SerializeField] private Popup _popup;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void OpenPopup(ItemController owner) {
        _popup.gameObject.SetActive(true);
        _popup.InitPopup(owner);
    }
    public void ClosePopup() {
        _popup.gameObject.SetActive(false);
    }
}
