using UnityEngine;

public class IdleState : PlayerState {
  public IdleState(PlayerController player) : base(player) { }
  private Animator animator;
  private IInputProvider inputProvider;

  public override void Enter() {
    // Debug.Log("Entered Idle State");
    animator = player.GetComponent<Animator>();
    inputProvider = player.GetComponent<IInputProvider>();

    if (!CheckIdleState()) return;
    animator.SetBool("isWalking", false);
  }

  public override void Exit() {
    // Debug.Log("Exited Idle State");
  }


  public override void Update() {
    var moveInput = inputProvider.GetMoveInput();

    if (moveInput.x != 0 || moveInput.y != 0) {
      player.ChangeState(new MoveState(player));
    }
    else if (inputProvider.IsSprinting()) {
      //player.ChangeState(new SprintState(player));
    }
    else if (inputProvider.IsAttackPressed()) {
      //player.ChangeState(new AttackState(player));
    }
  }

  private bool CheckIdleState() {
    bool isValid = true;
    if (animator == null) {
      Debug.LogError("Animator component is missing.");
      isValid = false;
    }
    if (inputProvider == null) {
      Debug.LogError("IInputProvider component is missing.");
      isValid = false;
    }
    return isValid;
  }
}
