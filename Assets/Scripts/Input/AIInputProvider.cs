using UnityEngine;

public class AIInputProvider : MonoBehaviour, IInputProvider {
  [SerializeField] private Transform target; // Example: player target
  [SerializeField] private float attackRange = 2.5f;
  [SerializeField] private float sprintDistance = 5f;

  private Vector2 moveInput;
  private bool attackPressed;
  private bool isSprinting;

  private void Update() {
    if (target == null) return;

    Vector2 direction = (target.position - transform.position);
    float distance = direction.magnitude;
    direction.Normalize();
    moveInput = direction;

    if (distance <= attackRange) {
      moveInput = Vector2.zero; // Stop moving if within attack range
    }

    // Decision logic: attack when close
    attackPressed = distance <= attackRange;

    // Sprint if far
    isSprinting = distance > sprintDistance;
  }

  public Vector2 GetMoveInput() {
    return moveInput;
  }

  public bool IsAttackPressed() {
    return attackPressed;
  }

  public bool IsSprinting() {
    return isSprinting;
  }
}
