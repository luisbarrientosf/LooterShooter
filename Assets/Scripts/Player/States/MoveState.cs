using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveState : PlayerState {
  public MoveState(PlayerController player) : base(player) { }
  private readonly List<KeyCode> keysBeingHolded = new();
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
  private Animator animator;
  private IInputProvider inputProvider;
  private PlayerInput playerInput;
  private Rigidbody2D rb;

  public override void Enter() {
    // Debug.Log("Entered Move State");
    animator = player.GetComponent<Animator>();
    inputProvider = player.GetComponent<IInputProvider>();
    playerInput = player.GetComponent<PlayerInput>();
    rb = player.GetComponent<Rigidbody2D>();

    if (!CheckMoveState()) return;
    animator.SetBool("isWalking", true);
  }

  public override void Exit() {
    // Debug.Log("Exited Move State");
  }

  public override void Update() {
    if (!CheckMoveState()) return;
    if (inputProvider is HumanInputProvider && playerInput.currentControlScheme == "Keyboard&Mouse") {
      ManageKeyboardInput();
    }
    else {
      ManageInput();
    }

    if (player.GetMovementInput() == Vector2.zero) {
      player.ChangeState(new IdleState(player));
    }
  }

  public override void FixedUpdate() {
    Vector2 movementInput = player.GetMovementInput();
    if (!CheckMoveState()) return;

    if (movementInput != Vector2.zero) {
      rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime);
    }
  }

  private void UpdateAnimator(Vector2 direction) {
    animator.SetFloat("walkingX", direction.x);
    animator.SetFloat("walkingY", direction.y);
  }

  private void UpdateAnimator(Vector2 currentDirection, bool up, bool down, bool left, bool right) {
    if (up && down) {
      animator.SetFloat("walkingY", currentDirection.y);
      animator.SetFloat("walkingX", 0);
    }
    else if (left && right) {
      animator.SetFloat("walkingX", currentDirection.x);
      animator.SetFloat("walkingY", 0);
    }
    else {
      animator.SetFloat("walkingX", currentDirection.x);
      animator.SetFloat("walkingY", currentDirection.y);
    }
  }

  private void ManageKeyboardInput() {
    UpdateKeysBeingHolded();

    Vector2 currentDirection = GetCurrentDirection();
    bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

    UpdateAnimator(currentDirection, up, down, left, right);

    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    bool hasInverseDirections = (up && down) || (left && right);

    bool isSprinting = inputProvider.IsSprinting();
    float sprintMultiplier = isSprinting ? player.sprintSpeed / player.speed : 1f;

    Vector2 inputVector = new Vector2(horizontal, vertical).normalized;
    Vector2 movementInput = hasInverseDirections ? currentDirection : inputVector;
    movementInput *= player.speed * sprintMultiplier;

    player.SetMovementInput(movementInput);
  }

  private void ManageInput() {
    Vector2 rawInput = inputProvider.GetMoveInput();
    Vector2 currentDirection = rawInput.normalized;

    bool isSprinting = inputProvider.IsSprinting();
    float sprintMultiplier = isSprinting ? player.sprintSpeed / player.speed : 1f;

    Vector2 movementInput = currentDirection * player.speed * sprintMultiplier;

    player.SetMovementInput(movementInput);
    UpdateAnimator(currentDirection);
  }



  private Vector2 GetCurrentDirection() {
    Vector2 currentDirection = Vector2.zero;
    for (int i = keysBeingHolded.Count - 1; i >= 0; i--) {
      KeyCode key = keysBeingHolded[i];
      if (Input.GetKey(key)) {
        currentDirection = keysDictionary[key];
        break;
      }
    }
    return currentDirection;
  }

  private void UpdateKeysBeingHolded() {
    foreach (var pair in keysDictionary) {
      if (Input.GetKeyDown(pair.Key)) {
        keysBeingHolded.Remove(pair.Key);
        keysBeingHolded.Add(pair.Key);
      }

      if (Input.GetKeyUp(pair.Key)) {
        keysBeingHolded.Remove(pair.Key);
      }
    }
  }

  private bool CheckMoveState() {
    bool isValid = true;
    if (player == null) {
      Debug.LogError("PlayerController is not assigned.");
      isValid = false;
    }
    if (animator == null) {
      Debug.LogError("Animator is not assigned.");
      isValid = false;
    }
    if (inputProvider == null) {
      Debug.LogError("InputProvider is not assigned.");
      isValid = false;
    }
    else {
      if (inputProvider is HumanInputProvider && playerInput == null) {
        Debug.LogError("PlayerInput is not assigned.");
        isValid = false;
      }
    }

    if (rb == null) {
      Debug.LogError("Rigidbody2D is not assigned.");
      isValid = false;
    }

    return isValid;
  }
}
