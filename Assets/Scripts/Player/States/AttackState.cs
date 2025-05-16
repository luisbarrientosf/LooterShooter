using UnityEngine;

public class AttackState : PlayerState {
  private float attackTimer;

  public AttackState(PlayerController player) : base(player) { }


  public override void Enter() {
    Debug.Log("Entered Attack State");

    // Call combat logic
    // player.Attack();

    // Trigger animation
    //   if (player.animator)
    //     player.animator.SetTrigger("attack");

    //   attackTimer = player.attackDuration;
    //   player.SetHorizontalVelocity(0); // stop movement during attack
    // }
  }

  public override void Exit() {
    Debug.Log("Exited Attack State");
    // Reset attack timer
    // attackTimer = 0f;

    // // Reset animation
    // //   if (player.animator)
    // //     player.animator.ResetTrigger("attack");
  }

  public override void Update() {
    IInputProvider input = player.GetComponent<IInputProvider>();
    attackTimer -= Time.deltaTime;

    if (attackTimer <= 0f) {
      // Transition back based on input
      Vector2 move = input.GetMoveInput();

      if (move.x != 0)
        player.ChangeState(new MoveState(player));
      else
        player.ChangeState(new IdleState(player));
    }
  }
}
