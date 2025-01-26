using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set;}
    public List<Item> items = new List<Item>();
    public Transform inventory;
    public GameObject inventoryItem;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void Add(Item item) {
        items.Add(item);
    }

    void Remove(Item item) {
        items.Remove(item);
    }


}
