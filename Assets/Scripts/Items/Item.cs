using UnityEngine;

public enum ItemType {
  Consumable,
  Equipable,
  Quest
}

public class Item : ScriptableObject {
  public string itemName;
  public string description;
  public Sprite icon;
  public ItemType itemType;

  public virtual void Use(PlayerInventory player) {
    Debug.Log("Used " + itemName);
  }
}
