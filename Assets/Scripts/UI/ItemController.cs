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
        InitializeItem(item, Count);
    }

    public void InitializeItem(Item newItem, int count) {
        item = newItem;
        image.sprite = item.icon;
        itemName.text = item.itemName;
        Count = count;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        transform.SetParent(transform.root);
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData) {
        MoveToParent();
        image.raycastTarget = true;
    }

    public void MoveToParent() {
        transform.SetParent(parentSlot.transform);
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
            int overflow = Count + addedCount - item.maxStack;
            Count += addedCount - overflow;
            CreateNewStack(overflow);
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

    private void CreateNewStack(int count) {
        InventorySlot freeSlot = InventoryManager.Instance.GetFirstFreeSlot();
        if (freeSlot != null) {
            ItemController newStack = Instantiate(this.gameObject, freeSlot.transform).GetComponent<ItemController>();
            newStack.InitializeItem(this.item, count);
        }
    }

    private void UpdateCountText() {
        if (Count > 1) {
            countText.text = Count.ToString();            
        } else {
            countText.text = "";
        }
 
    }

    void OnDestroy() {
        if (item is Apparel) {
            parentSlot.OnItemLeft();
            PlayerEventManager.Instance.InvokeItemEquipStatusChanged(item, false);
        }
    }
}
