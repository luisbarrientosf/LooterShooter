using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
  public float speed = 5f;
  public float sprintSpeed = 10f;

  private IInputProvider inputProvider;
  private Rigidbody2D rb;
  private Animator animator;

  private Vector2 movementInput = Vector2.zero;

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

  void Awake() {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    inputProvider = GetComponent<IInputProvider>();

    CheckPlayerController();
  }

  void Update() {
    if (!CheckPlayerController()) return;

    var playerInput = GetComponent<PlayerInput>();
    if (playerInput.currentControlScheme == "Keyboard&Mouse") {

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
      float sprintMultiplier = isSprinting ? sprintSpeed / speed : 1f;

      Vector2 inputVector = new Vector2(horizontal, vertical).normalized;
      movementInput = hasInverseDirections ? currentDirection : inputVector;
      movementInput *= speed * sprintMultiplier;
    }
    else {
      Vector2 rawInput = inputProvider.GetMoveInput();
      Vector2 currentDirection = rawInput.normalized;

      bool isSprinting = inputProvider.IsSprinting();
      float sprintMultiplier = isSprinting ? sprintSpeed / speed : 1f;

      movementInput = currentDirection * speed * sprintMultiplier;

      UpdateAnimator(currentDirection);
    }
  }

  void FixedUpdate() {
    if (movementInput != Vector2.zero) {
      rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime);
    }
  }

  private void UpdateAnimator(Vector2 direction) {
    bool isWalking = direction != Vector2.zero;
    animator.SetBool("isWalking", isWalking);
    animator.SetFloat("walkingX", direction.x);
    animator.SetFloat("walkingY", direction.y);
  }

  private void UpdateAnimator(Vector2 currentDirection, bool up, bool down, bool left, bool right) {
    bool isWalking = currentDirection != Vector2.zero;
    animator.SetBool("isWalking", isWalking);

    if (up && down) {
      animator.SetFloat("walkingY", currentDirection.y);
      animator.SetFloat("walkingX", 0);
    }
    else if (left && right) {
      animator.SetFloat("walkingX", currentDirection.x);
      animator.SetFloat("walkingY", 0);
    }
    else if (isWalking) {
      animator.SetFloat("walkingX", currentDirection.x);
      animator.SetFloat("walkingY", currentDirection.y);
    }
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


  private bool CheckPlayerController() {
    if (rb == null) {
      Debug.LogError("Rigidbody2D component is missing.");
      return false;
    }

    if (animator == null) {
      Debug.LogError("Animator component is missing.");
      return false;
    }

    if (inputProvider == null) {
      Debug.LogError("IInputProvider component is missing.");
      return false;
    }

    return true;
  }
}
