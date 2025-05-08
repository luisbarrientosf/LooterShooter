using UnityEngine;

public class TargetFrameRate : MonoBehaviour {
  void Awake() {
    Application.targetFrameRate = 60;
  }
}
