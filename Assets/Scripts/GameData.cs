using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerHp;
    public int enemyHp;
    public SlotData[] inventory;
    public ItemData[] equippedArmor;
    public GameData(int playerHp, int enemyHp, SlotData[] inventory, ItemData[] equippedArmor) {
        this.playerHp = playerHp;
        this.enemyHp = enemyHp;
        this.inventory = inventory;
        this.equippedArmor = equippedArmor;
    }
}
[System.Serializable]
public class ItemData
{
    public string itemName;
    public int count;
    public int itemId;
    public ItemData(string itemName, int count, int itemId) {
        this.itemName = itemName;
        this.count = count;
        this.itemId = itemId;
    }
}
[System.Serializable]
public class SlotData
{
    public ItemData itemData;
    public SlotData(ItemData itemData) {
        this.itemData = itemData;
    }
}
