using UnityEngine;

public class SprintState : PlayerState {
  public SprintState(PlayerController player) : base(player) { }

  public override void Enter() {
    Debug.Log("Entered Sprint State");
  }

  public override void Exit() {
    Debug.Log("Exited Sprint State");
  }

  public override void Update() {
    var input = player.GetComponent<IInputProvider>();
    Vector2 move = input.GetMoveInput();

    if (!input.IsSprinting()) {
      // Go back to move or idle depending on input
      if (move.x == 0)
        player.ChangeState(new IdleState(player));
      else
        player.ChangeState(new MoveState(player));
      return;
    }

    // Movement input is still valid, continue sprinting
    //player.SetHorizontalVelocity(move.x * player.moveSpeed * player.sprintMultiplier);


    if (input.IsAttackPressed()) {
      player.ChangeState(new AttackState(player));
    }
  }
}
