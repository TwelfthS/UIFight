using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance { get; private set;}
    [field: SerializeField] public Item[] awailableLoot { get; private set; }
    [SerializeField] private int _lootCountToGive = 1;

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
            stack.IncreaseCount(_lootCountToGive);
        } else {
            InventorySlot freeSlot = InventoryManager.Instance.GetFirstFreeSlot();
            GameManager.Instance.CreateItem(lootToSpawn, _lootCountToGive, freeSlot.transform);
        }
        Debug.Log("Received " + _lootCountToGive + " " + lootToSpawn.itemName + " as loot");
    }
}
