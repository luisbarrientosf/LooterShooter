using UnityEngine;

public class ScreenToggle : MonoBehaviour {
  public KeyCode toggleKey = KeyCode.F11;
  public int windowedWidth = 1280;
  public int windowedHeight = 720;

  void Update() {
    if (Input.GetKeyDown(toggleKey)) {
      ToggleFullscreen();
    }
  }

  void ToggleFullscreen() {
    if (Screen.fullScreen) {
      Screen.SetResolution(windowedWidth, windowedHeight, FullScreenMode.Windowed);
    }
    else {
      Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, FullScreenMode.FullScreenWindow);
    }
  }
}
