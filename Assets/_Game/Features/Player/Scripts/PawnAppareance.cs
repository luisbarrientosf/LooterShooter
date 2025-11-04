using UnityEngine;

public class PawnAppearance : MonoBehaviour {

  public SpriteRenderer rightEyeRenderer;
  public SpriteRenderer leftEyeRenderer;
  public SpriteRenderer hatRenderer;

  public GameObject[] leftEyesVariants;
  public GameObject[] rightEyesVariants;
  public EquipableItem[] hatsVariants;

  void Start() {
    leftEyesVariants = Resources.LoadAll<GameObject>("Sprites/Player/Eyes/LeftEyes");
    rightEyesVariants = Resources.LoadAll<GameObject>("Sprites/Player/Eyes/RightEyes");
    hatsVariants = Resources.LoadAll<EquipableItem>("Hats");

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
    AttachEquippableItem(hatRenderer, hatsVariants[randomHatIndex]);
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

  public void AttachEquippableItem(SpriteRenderer targetRenderer, EquipableItem item) {
    targetRenderer.sprite = item.sprite;

    if (item.appearanceOptions == null) return;

    targetRenderer.transform.localPosition = item.appearanceOptions.correctLocalPosition;
    targetRenderer.transform.localScale = item.appearanceOptions.correctLocalScale;
    targetRenderer.transform.localEulerAngles = item.appearanceOptions.correctLocalRotation;
  }
}