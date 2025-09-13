using System;
using UnityEngine;

public class PawnBounceAnimation : MonoBehaviour {
  public Animator animator;
  public Transform bounceTarget;
  public float bounceSpeed = 20f;
  public float bounceAmount = 0.12f;

  private Vector3 startPos;
  public float bounceTimer;
  private bool isWalking = false;

  void Start() {
    startPos = bounceTarget.localPosition;
  }

  void Update() {
    isWalking = animator.GetBool("isWalking");

    if (isWalking) {
      bounceTimer += Time.deltaTime * bounceSpeed;
      float bounceY = Math.Abs(Mathf.Sin(bounceTimer) * bounceAmount);
      bounceTarget.localPosition = startPos + new Vector3(0, bounceY, 0);
    }
    else {
      bounceTarget.localPosition = Vector3.Lerp(bounceTarget.localPosition, startPos, Time.deltaTime * 10f);
      bounceTimer = 0f;
    }
  }

  public bool IsWalking() {
    return isWalking;
  }
}