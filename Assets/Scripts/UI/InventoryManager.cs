using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set;}
    public List<InventorySlot> slots {get; private set;} = new List<InventorySlot>();
    [SerializeField] private Transform inventory;    

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        foreach (Transform child in inventory) {
            InventorySlot slot = child.gameObject.GetComponent<InventorySlot>();
            if (slot) {
                slots.Add(slot);
            }
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

}
