using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance { get; private set;}
    [field: SerializeField] public Item[] awailableLoot { get; private set; }
    [SerializeField] private GameObject inventoryItemPrefab;
    private int lootCountToGive = 1;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }

    public void SpawnRandomLoot() {
        int randomIndex = Random.Range(0, awailableLoot.Length);
        Item lootToSpawn = awailableLoot[randomIndex];
        ItemController stack = InventoryManager.Instance.FindStackWithItem(lootToSpawn);
        if (stack != null) {
            stack.IncreaseCount(lootCountToGive);
        } else {
            InventorySlot freeSlot = InventoryManager.Instance.GetFirstFreeSlot();
            ItemController newItem = Instantiate(inventoryItemPrefab, freeSlot.transform).GetComponent<ItemController>();
            newItem.InitializeItem(lootToSpawn, lootCountToGive);
        }
    }
}
