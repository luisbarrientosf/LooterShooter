using UnityEngine;

public class PawnAppearance : MonoBehaviour {

  public SpriteRenderer rightEyeRenderer;
  public SpriteRenderer leftEyeRenderer;
  public SpriteRenderer hatRenderer;

  public GameObject[] leftEyesVariants;
  public GameObject[] rightEyesVariants;
  public GameObject[] hatsVariants;

  public void ChangeEyes(int leftEyeIndex, int rightEyeIndex) {
    if (leftEyeRenderer != null && leftEyesVariants.Length > 0 && leftEyeIndex < leftEyesVariants.Length) {
      leftEyeRenderer.sprite = leftEyesVariants[leftEyeIndex].GetComponent<SpriteRenderer>().sprite;
    }

    if (rightEyeRenderer != null && rightEyesVariants.Length > 0 && rightEyeIndex < rightEyesVariants.Length) {
      rightEyeRenderer.sprite = rightEyesVariants[rightEyeIndex].GetComponent<SpriteRenderer>().sprite;
    }
  }

  public void ChangeHat(int hatIndex) {
    if (hatRenderer != null && hatsVariants.Length > 0 && hatIndex < hatsVariants.Length) {
      hatRenderer.sprite = hatsVariants[hatIndex].GetComponent<SpriteRenderer>().sprite;
      hatRenderer.transform.localPosition = hatsVariants[hatIndex].GetComponent<AppearanceItem>().correctLocalPosition;
      hatRenderer.transform.localScale = hatsVariants[hatIndex].GetComponent<AppearanceItem>().correctLocalScale;
    }
  }

  void Start() {

    leftEyesVariants = Resources.LoadAll<GameObject>("Sprites/Player/Eyes/LeftEyes");
    rightEyesVariants = Resources.LoadAll<GameObject>("Sprites/Player/Eyes/RightEyes");
    hatsVariants = Resources.LoadAll<GameObject>("Sprites/Player/Hats");

    int randomEyeIndex = Random.Range(0, leftEyesVariants.Length);
    ChangeEyes(randomEyeIndex, randomEyeIndex);
    int randomHatIndex = Random.Range(0, hatsVariants.Length);
    ChangeHat(randomHatIndex);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.R)) {
      int randomPosition = Random.Range(0, leftEyesVariants.Length);
      ChangeEyes(randomPosition, randomPosition);
      int randomHatIndex = Random.Range(0, hatsVariants.Length);
      ChangeHat(randomHatIndex);
      Start();
    }
  }
}