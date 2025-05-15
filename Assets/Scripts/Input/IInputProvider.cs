using UnityEngine;

public interface IInputProvider {
  Vector2 GetMoveInput();
  bool IsAttackPressed();
  bool IsSprinting();
  // bool IsInteractPressed();
  // bool IsPausePressed();
  // bool IsInventoryPressed();
}