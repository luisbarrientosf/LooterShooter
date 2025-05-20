using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class HumanInputProvider : MonoBehaviour, IInputProvider {
  private PlayerInput playerInput;
  private InputAction moveAction;
  private InputAction attackAction;
  private InputAction sprintAction;

  private readonly List<KeyCode> keysBeingHeld = new();
  private readonly Dictionary<KeyCode, Vector2> keysDictionary = new() {
    { KeyCode.W, Vector2.up },
    { KeyCode.UpArrow, Vector2.up },
    { KeyCode.S, Vector2.down },
    { KeyCode.DownArrow, Vector2.down },
    { KeyCode.A, Vector2.left },
    { KeyCode.LeftArrow, Vector2.left },
    { KeyCode.D, Vector2.right },
    { KeyCode.RightArrow, Vector2.right }
  };

  private void Awake() {
    playerInput = GetComponent<PlayerInput>();
    moveAction = playerInput.actions["Move"];
    attackAction = playerInput.actions["Attack"];
    sprintAction = playerInput.actions["Sprint"];
    CheckHumanInputProvider();
  }

  private void Update() {
    UpdateKeysBeingHeld();
  }

  public bool IsAttackPressed() {
    return attackAction.WasPressedThisFrame();
  }

  public bool IsSprinting() {
    return sprintAction.IsPressed();
  }

  public Vector2 GetMoveInput() {
    if (playerInput.currentControlScheme == "Keyboard&Mouse") {
      bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
      bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
      bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
      bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
      bool hasInverseDirections = (up && down) || (left && right);

      if (hasInverseDirections) {
        return GetCurrentDirection();
      }

      float horizontal = Input.GetAxisRaw("Horizontal");
      float vertical = Input.GetAxisRaw("Vertical");
      return new Vector2(horizontal, vertical);
    }
    else {
      return moveAction.ReadValue<Vector2>();
    }
  }

  public Vector2 GetCurrentDirection() {
    // Use last pressed key direction
    for (int i = keysBeingHeld.Count - 1; i >= 0; i--) {
      KeyCode key = keysBeingHeld[i];
      if (Input.GetKey(key)) {
        return keysDictionary[key];
      }
    }

    return Vector2.zero;
  }

  private void UpdateKeysBeingHeld() {
    foreach (var pair in keysDictionary) {
      if (Input.GetKeyDown(pair.Key)) {
        keysBeingHeld.Remove(pair.Key); // Ensure it’s at the end
        keysBeingHeld.Add(pair.Key);
      }

      if (Input.GetKeyUp(pair.Key)) {
        keysBeingHeld.Remove(pair.Key);
      }
    }
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
