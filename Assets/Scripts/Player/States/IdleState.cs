using UnityEngine;

public class IdleState : PlayerState {
  public IdleState(PlayerController player) : base(player) { }

  public override void Enter() {
    Debug.Log("Entered Idle State");
  }

  public override void Exit() {
    Debug.Log("Exited Idle State");
  }


  public override void Update() {
    IInputProvider input = player.GetComponent<IInputProvider>();
    var moveInput = input.GetMoveInput();

    if (moveInput.x != 0 || moveInput.y != 0) {
      player.ChangeState(new MoveState(player));
    }
    else if (input.IsSprinting()) {
      //player.ChangeState(new SprintState(player));
    }
    else if (input.IsAttackPressed()) {
      //player.ChangeState(new AttackState(player));
    }
  }
}
