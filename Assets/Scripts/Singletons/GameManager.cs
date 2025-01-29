using System.IO;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    private string filePath;
    [field:SerializeField] public PlayerManager player { get; private set; }
    [field:SerializeField] public EnemyManager enemy { get; private set; }
    [SerializeField] private Item[] items;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject gameOverScreen;
    private bool isGameOver = false;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        if (File.Exists(filePath))
        {
            LoadData();
        }
    }

    public ItemController LoadItem(ItemData itemData) {
        if (itemData != null && itemData.itemName != "") {
            Item item = Array.Find(items, (obj) => obj.id == itemData.itemId);
            if (item != null) {
                return CreateItem(item, itemData.count, null);
            }
        }
        return null;
    }

    public ItemController CreateItem(Item item, int count, Transform parent) {
        ItemController newItem = Instantiate(inventoryItemPrefab, parent).GetComponent<ItemController>();
        newItem.InitializeItem(item, count);
        if (parent) {
            InventorySlot slot = parent.GetComponent<InventorySlot>();
            if (slot) slot.SetContent(newItem);            
        }
        return newItem;
    }

    public void SaveData(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filePath, json);
    }

    public void LoadData()
    {
        string json = File.ReadAllText(filePath);
        GameData loadedData = JsonUtility.FromJson<GameData>(json);
        if (loadedData != null) {
            player.LoadHP(loadedData.playerHp);
            enemy.LoadHP(loadedData.enemyHp);
            InventoryManager.Instance.LoadInventory(loadedData.inventory);
            ArmorManager.Instance.LoadArmor(loadedData.equippedArmor);
        }
    }

    public void GameOver() {
        gameOverScreen.SetActive(true);
        isGameOver = true;
    }

    private GameData CollectData() {
        int playerHp = player.GetHP();
        int enemyHp = enemy.GetHP();
        SlotData[] inventory = InventoryManager.Instance.CollectData();
        ItemData[] equippedArmor = ArmorManager.Instance.GetEquippedArmor();
        GameData gameData = new GameData(playerHp, enemyHp, inventory, equippedArmor);
        return gameData;
    }

    private void OnGameClosed() {
        if (!isGameOver) {
            GameData savedData = CollectData();
            SaveData(savedData); 
        }
    }

    void OnApplicationQuit()
    {
        OnGameClosed();
    }

    // for testing in the editor
    void OnDestroy() {
        OnGameClosed();
    }
}