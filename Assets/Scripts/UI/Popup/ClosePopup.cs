using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePopup : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        PopupManager.Instance.ClosePopup();
    }
}
