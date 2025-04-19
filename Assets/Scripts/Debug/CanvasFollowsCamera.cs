using UnityEngine;

public class CanvasFollowsCamera : MonoBehaviour
{
  private Camera cam;

  void Start()
  {
    cam = Camera.main;
  }

  void LateUpdate()
  {
    transform.forward = cam.transform.forward;
  }
}

