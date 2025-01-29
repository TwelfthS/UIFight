using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set;}
    public List<InventorySlot> slots {get; private set;} = new List<InventorySlot>();
    [SerializeField] private Transform inventory;
    private bool isInventoryCreated = false;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        if (!isInventoryCreated) {
            InitInventory();
        }
    }

    public InventorySlot GetFirstFreeSlot() {
        foreach (InventorySlot slot in slots) {
            if (slot.content == null) {
                return slot;
            }
        }
        return null;
    }

    public ItemController FindStackWithItem(Item item) {
        foreach (InventorySlot slot in slots) {
            if (slot.content != null && slot.content.item == item) {
                return slot.content;
            }
        }
        return null;
    }

    public SlotData[] CollectData() {
        SlotData[] data = new SlotData[slots.Count];
        for (int i = 0; i < slots.Count; i++) {
            if (slots[i].content == null) {
                data[i] = new SlotData(null);
            } else {
                data[i] = new SlotData(ItemControllerToData(slots[i].content));
            }
        }
        return data;
    }

    public ItemData ItemControllerToData(ItemController item) {
        if (item != null && item.item != null) {
            string name = item.item.itemName;
            int count = item.Count;
            int id = item.item.id;
            ItemData itemData = new ItemData(name, count, id);
            return itemData;            
        } else {
            return null;
        }
    }

    public void LoadInventory(SlotData[] slotData) {
        if (!isInventoryCreated) {
            InitInventory();
        }
        CleanInventory();
        for (int i = 0; i < slotData.Length; i++) {
            if (slotData[i]?.itemData?.itemName != "") {
                ItemController item = GameManager.Instance.LoadItem(slotData[i].itemData);
                slots[i].SetContent(item);
            }
        }
    }

    public void CleanInventory() {
        for (int i = 0; i < slots.Count; i++) {
            slots[i].CleanUp();
        }
    }

    private void InitInventory() {
        foreach (Transform child in inventory) {
            InventorySlot slot = child.gameObject.GetComponent<InventorySlot>();
            if (slot) {
                slots.Add(slot);
                slot.InitSlot();
            }
        }
        isInventoryCreated = true;
    }
}
