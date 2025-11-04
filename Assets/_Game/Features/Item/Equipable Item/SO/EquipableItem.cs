using UnityEngine;

public enum EquipmentSlot {
  Head,
  Body,
  Weapon,
  Accessory
}

[CreateAssetMenu(fileName = "NewEquipableItem", menuName = "Items/Equipable")]

public class EquipableItem : Item {
  public EquipmentSlot slot;
  public AppearanceItem appearanceOptions;
  public int attackBonus;
  public int defenseBonus;

  public override void Use(PlayerInventory player) {
    player.Equip(this);
  }
}
