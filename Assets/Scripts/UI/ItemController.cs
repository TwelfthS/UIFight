using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public Image image;
    [HideInInspector] public InventorySlot parentSlot;
    [SerializeField] private int _count = 1;
    public int Count {
        get { return _count; }
        private set {
            _count = value;
            UpdateCountText();
        }
    }
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text itemName;

    void Awake() {
        parentSlot = transform.parent.GetComponent<InventorySlot>();
    }

    void Start() {
        InitializeItem(item);
    }

    public void InitializeItem(Item newItem) {
        item = newItem;
        image.sprite = item.icon;
        itemName.text = item.itemName;
        UpdateCountText();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        transform.SetParent(transform.root);
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentSlot.transform);
        image.raycastTarget = true;
    }

    public void UseItem() {
        if (item is Medical) {
            PlayerEventManager.Instance.InvokeItemUsed(item);
            ReduceCount(1);            
        } else if (item is Ammo) {
            IncreaseCount(1);
        } else if (item is Apparel) {
            ArmorManager.Instance.EquipArmor(this);
        }
    }

    public void IncreaseCount(int addedCount) {
        if (Count + addedCount > item.maxStack) {
            // create new item
        } else {
            Count += addedCount;
        }
    }

    public void ReduceCount(int removedCount) {
        Count = Mathf.Max(Count - removedCount, 0);
        if (Count == 0) {
            Destroy(this.gameObject);
        }
    }

    private void UpdateCountText() {
        countText.text = Count.ToString();
    }

    void OnDestroy() {
        if (item is Apparel) {
            PlayerEventManager.Instance.InvokeItemEquipStatusChanged(item, false);
        }
    }
}
