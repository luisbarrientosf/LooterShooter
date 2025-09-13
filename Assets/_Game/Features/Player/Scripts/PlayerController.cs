using UnityEngine;
public class PlayerController : MonoBehaviour {
  public float speed = 5f;
  public float sprintSpeed = 10f;

  private PlayerState currentState;
  private IInputProvider inputProvider;
  private Rigidbody2D rb;
  private Animator animator;

  private Vector2 movementInput = Vector2.zero;

  void Awake() {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    inputProvider = GetComponent<IInputProvider>();

    CheckPlayerController();
  }

  void Start() {
    ChangeState(new IdleState(this));
  }

  void Update() {
    if (!CheckPlayerController()) return;
    currentState?.Update();
  }

  void FixedUpdate() {
    currentState?.FixedUpdate();
  }

  public void ChangeState(PlayerState newState) {
    currentState?.Exit();
    currentState = newState;
    currentState.Enter();
  }

  public void SetMovementInput(Vector2 input) {
    movementInput = input;
  }

  public Vector2 GetMovementInput() {
    return movementInput;
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
