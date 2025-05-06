using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
  public int coinCount = 0;

  public List<Item> items = new();

  // One item per slot
  public Dictionary<EquipmentSlot, EquipableItem> equippedItems = new();

  public void AddItem(Item item) {
    items.Add(item);
    Debug.Log("Item added: " + item.itemName);
  }

  public void Equip(EquipableItem item) {
    if (equippedItems.ContainsKey(item.slot)) {
      Debug.Log("Unequipped: " + equippedItems[item.slot].itemName);
    }

    equippedItems[item.slot] = item;
    Debug.Log("Equipped: " + item.itemName + " in slot: " + item.slot);
    // TODO: Apply bonuses to player stats
  }

  public void AddCoin(int amount = 1) {
    coinCount += amount;
    Debug.Log("Coin collected! Total: " + coinCount);
  }

  public int GetCoinCount() {
    return coinCount;
  }
}
