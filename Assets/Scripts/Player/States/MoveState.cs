using UnityEngine;

public class MoveState : PlayerState {
  public MoveState(PlayerController player) : base(player) { }

  private Animator animator;
  private IInputProvider inputProvider;
  private Rigidbody2D rb;

  public override void Enter() {
    // Debug.Log("Entered Move State");
    animator = player.GetComponent<Animator>();
    inputProvider = player.GetComponent<IInputProvider>();
    rb = player.GetComponent<Rigidbody2D>();

    if (!CheckMoveState()) return;
    animator.SetBool("isWalking", true);
  }

  public override void Exit() {
    // Debug.Log("Exited Move State");
  }

  public override void Update() {
    if (!CheckMoveState()) return;

    Vector2 rawInput = inputProvider.GetMoveInput();
    Vector2 currentDirection = rawInput.normalized;

    bool isSprinting = inputProvider.IsSprinting();
    float sprintMultiplier = isSprinting ? player.sprintSpeed / player.speed : 1f;

    Vector2 movementInput = currentDirection * player.speed * sprintMultiplier;

    player.SetMovementInput(movementInput);
    UpdateAnimator(currentDirection);

    if (player.GetMovementInput() == Vector2.zero) {
      player.ChangeState(new IdleState(player));
    }
  }

  public override void FixedUpdate() {
    if (!CheckMoveState()) return;

    Vector2 movementInput = player.GetMovementInput();
    if (movementInput != Vector2.zero) {
      rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime);
    }
  }

  private void UpdateAnimator(Vector2 direction) {
    animator.SetFloat("walkingX", direction.x);
    animator.SetFloat("walkingY", direction.y);
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
    if (rb == null) {
      Debug.LogError("Rigidbody2D is not assigned.");
      isValid = false;
    }

    return isValid;
  }
}
