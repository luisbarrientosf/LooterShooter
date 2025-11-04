using UnityEngine;

public class AppearanceItem : MonoBehaviour {
  public Vector3 correctLocalPosition;
  public Vector3 correctLocalRotation;
  public Vector3 correctLocalScale;

  void OnValidate() {
    correctLocalPosition = transform.localPosition;
    correctLocalRotation = transform.localEulerAngles;
    correctLocalScale = transform.localScale;
  }
}