using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [field:SerializeField] public Item item { get; private set; }
    [HideInInspector] public InventorySlot parentSlot;
    [HideInInspector] public Transform parentAfterDrag;
    public event Action goneFromSlot;
    [SerializeField] private Image _image;
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
        // if (transform.parent != null) {
        //     parentSlot = transform.parent.GetComponent<InventorySlot>();
        // }
    }

    void Start() {
        InitializeItem(item, Count);
    }

    public void InitializeItem(Item newItem, int count) {
        item = newItem;
        _image.sprite = item.icon;
        itemName.text = item.itemName;
        Count = count;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentAfterDrag);
        _image.raycastTarget = true;
    }

    // public void MoveToParent() {
    //     // transform.SetParent(parentSlot.transform);
        
    // }

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

    public void InvokeGoneFromSlot() {
        goneFromSlot?.Invoke();
    }

    private void CreateNewStack(int count) {
        InventorySlot freeSlot = InventoryManager.Instance.GetFirstFreeSlot();
        if (freeSlot != null) {
            GameManager.Instance.CreateItem(item, count, freeSlot.transform);
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
        goneFromSlot?.Invoke();
        // parentSlot.OnItemLeft();
            // PlayerEventManager.Instance.InvokeItemEquipStatusChanged(item, false);
    }
}
