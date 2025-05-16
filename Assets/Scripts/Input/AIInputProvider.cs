using UnityEngine;

public class AIInputProvider : MonoBehaviour, IInputProvider {
  [SerializeField] private Transform target;
  [SerializeField] private float attackRange = 4f;
  [SerializeField] private float sprintDistance = 6f;
  [SerializeField] private float idleDelay = 1f;
  [SerializeField] private float attackCooldown = 0.7f;

  private Vector2 moveInput;
  private bool attackPressed;
  private bool isSprinting;

  private float idleTimer = 0f;
  private bool isInIdleDelay = false;
  private float lastAttackTime = -Mathf.Infinity;

  void Awake() {
    CheckAIInputProvider();
  }

  private void Update() {
    if (target == null) return;

    Vector2 direction = target.position - transform.position;
    float distance = direction.magnitude;
    direction.Normalize();

    if (distance <= attackRange) {
      moveInput = Vector2.zero;

      if (Time.time - lastAttackTime >= attackCooldown) {
        attackPressed = true;
        lastAttackTime = Time.time;

        if (!isInIdleDelay) {
          isInIdleDelay = true;
          idleTimer = 0f;
        }
      }
      else {
        attackPressed = false;
      }
    }
    else {
      if (isInIdleDelay) {
        idleTimer += Time.deltaTime;
        moveInput = Vector2.zero;

        if (idleTimer >= idleDelay) {
          isInIdleDelay = false;
        }
      }
      else {
        moveInput = direction;
        attackPressed = false;
      }
    }

    isSprinting = distance > sprintDistance;
  }

  public Vector2 GetMoveInput() => moveInput;
  public bool IsAttackPressed() => attackPressed;
  public bool IsSprinting() => isSprinting;

  // ✅ Draw debug gizmos in the Scene view
  private void OnDrawGizmos() {
    // Attack range - red
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, attackRange);

    // Sprint distance - yellow
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, sprintDistance);

    // Optional: draw a line to target
    if (target != null) {
      Gizmos.color = Color.green;
      Gizmos.DrawLine(transform.position, target.position);
    }
  }

  bool CheckAIInputProvider() {
    bool isValid = true;
    if (target == null) {
      Debug.LogError("Target is not assigned.");
      isValid = false;
    }
    return isValid;
  }
}
