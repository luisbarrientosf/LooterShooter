using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class HumanInputProvider : MonoBehaviour, IInputProvider {
  private PlayerInput playerInput;
  private InputAction moveAction;
  private InputAction attackAction;
  private InputAction sprintAction;

  private void Awake() {
    playerInput = GetComponent<PlayerInput>();
    moveAction = playerInput.actions["Move"];
    attackAction = playerInput.actions["Attack"];
    sprintAction = playerInput.actions["Sprint"];
    CheckHumanInputProvider();
  }

  public Vector2 GetMoveInput() {
    return moveAction.ReadValue<Vector2>();
  }

  public bool IsAttackPressed() {
    return attackAction.WasPressedThisFrame();
  }

  public bool IsSprinting() {
    return sprintAction.IsPressed();
  }

  private bool CheckHumanInputProvider() {
    bool isValid = true;
    if (playerInput == null) {
      Debug.LogError("PlayerInput component is missing.");
      isValid = false;
    }
    if (moveAction == null) {
      Debug.LogError("Move action is not assigned.");
      isValid = false;
    }
    if (attackAction == null) {
      Debug.LogError("Attack action is not assigned.");
      isValid = false;
    }
    if (sprintAction == null) {
      Debug.LogError("Sprint action is not assigned.");
      isValid = false;
    }
    return isValid;
  }
}
