using UnityEngine;

public class PawnAppearance : MonoBehaviour {

  public SpriteRenderer rightEyeRenderer;
  public SpriteRenderer leftEyeRenderer;
  public SpriteRenderer hatRenderer;

  public GameObject[] leftEyesVariants;
  public GameObject[] rightEyesVariants;
  public GameObject[] hatsVariants;

  void Start() {
    leftEyesVariants = Resources.LoadAll<GameObject>("Sprites/Player/Eyes/LeftEyes");
    rightEyesVariants = Resources.LoadAll<GameObject>("Sprites/Player/Eyes/RightEyes");
    hatsVariants = Resources.LoadAll<GameObject>("Sprites/Player/Hats");

    RandomizeAppearance();
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.X)) {
      RandomizeAppearance();
    }
  }

  void RandomizeAppearance() {
    int randomEyesIndex = Random.Range(0, leftEyesVariants.Length);
    AttachAppearanceItem(leftEyeRenderer, leftEyesVariants[randomEyesIndex]);
    AttachAppearanceItem(rightEyeRenderer, rightEyesVariants[randomEyesIndex]);

    int randomHatIndex = Random.Range(0, hatsVariants.Length);
    AttachAppearanceItem(hatRenderer, hatsVariants[randomHatIndex]);
  }

  public void AttachAppearanceItem(SpriteRenderer targetRenderer, GameObject itemPrefab) {
    targetRenderer.sprite = itemPrefab.GetComponent<SpriteRenderer>().sprite;
    AppearanceItem appareanceOptions = itemPrefab.GetComponent<AppearanceItem>();

    if (appareanceOptions != null) {
      targetRenderer.transform.localPosition = appareanceOptions.correctLocalPosition;
      targetRenderer.transform.localScale = appareanceOptions.correctLocalScale;
      targetRenderer.transform.localEulerAngles = appareanceOptions.correctLocalRotation;
    }
  }
}