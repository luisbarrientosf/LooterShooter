using UnityEngine;

public class PawnAppearance : MonoBehaviour {

  public SpriteRenderer rightEyeRenderer;
  public SpriteRenderer leftEyeRenderer;
  public GameObject[] leftEyesVariants;
  public GameObject[] rightEyesVariants;

  public void ChangeEyes(int leftEyeIndex, int rightEyeIndex) {
    if (leftEyeRenderer != null && leftEyesVariants.Length > 0 && leftEyeIndex < leftEyesVariants.Length) {
      leftEyeRenderer.sprite = leftEyesVariants[leftEyeIndex].GetComponent<SpriteRenderer>().sprite;
    }

    if (rightEyeRenderer != null && rightEyesVariants.Length > 0 && rightEyeIndex < rightEyesVariants.Length) {
      rightEyeRenderer.sprite = rightEyesVariants[rightEyeIndex].GetComponent<SpriteRenderer>().sprite;
    }
  }

  void Start() {

    leftEyesVariants = Resources.LoadAll<GameObject>("Sprites/Player/Eyes/LeftEyes");
    rightEyesVariants = Resources.LoadAll<GameObject>("Sprites/Player/Eyes/RightEyes");

    int randomPosition = Random.Range(0, leftEyesVariants.Length);
    ChangeEyes(randomPosition, randomPosition);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.R)) {
      int randomPosition = Random.Range(0, leftEyesVariants.Length);
      ChangeEyes(randomPosition, randomPosition);
      Start();
    }
  }
}