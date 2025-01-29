using UnityEngine;
using System;

public class PlayerEventManager : MonoBehaviour
{
    public static PlayerEventManager Instance { get; private set;}
    public event Action<Item> itemUsed;
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }
    public void InvokeItemUsed(Item item) {
        itemUsed?.Invoke(item);
    }
}
