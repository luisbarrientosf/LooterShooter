using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
  private Animator animator;
  public float speed = 5f;

  private readonly List<KeyCode> keysBeingHolded = new();
  private readonly Dictionary<KeyCode, Vector2> keysDictionary = new()
  {
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
    animator = GetComponent<Animator>();
  }

  void Update() {
    UpdateKeysBeingHolded();
    Vector2 currentDirection = GetCurrentDirection();

    // Update Animator
    bool isWalking = currentDirection != Vector2.zero;
    bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

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

    // Movement
    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    bool hasInverseDirections = (up && down) || (left && right);
    float sprintMultiplier = 1f;

    if (Input.GetKey(KeyCode.LeftShift)) {
      sprintMultiplier = 3f;
    }
    if (hasInverseDirections) {
      transform.Translate(sprintMultiplier * speed * Time.deltaTime * currentDirection);
    }
    else {
      Vector2 movement = new Vector2(horizontal, vertical);
      transform.Translate(sprintMultiplier * speed * Time.deltaTime * movement);
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
}