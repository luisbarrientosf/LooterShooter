using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteFlash : MonoBehaviour {
  [Header("Head Flash (Shader)")]
  public SpriteRenderer headRenderer;

  [Header("Body Flash (Color Tint)")]
  public SpriteRenderer[] bodyRenderers;

  [Header("Flash Settings")]
  public float flashDuration = 0.1f;
  public Color bodyFlashColor = Color.red;

  private Material headMaterial;
  private Color[] originalBodyColors;

  void Awake() {
    if (headRenderer != null)
      headMaterial = headRenderer.material;

    // Store original colors for body parts
    originalBodyColors = new Color[bodyRenderers.Length];
    for (int i = 0; i < bodyRenderers.Length; i++)
      originalBodyColors[i] = bodyRenderers[i].color;
  }

  public void Flash() {
    StopAllCoroutines();
    StartCoroutine(DoFlash());
  }

  private IEnumerator DoFlash() {
    // HEAD: Shader flash
    if (headMaterial != null)
      headMaterial.SetFloat("_FlashAmount", 1f);

    // BODY: Color tint
    for (int i = 0; i < bodyRenderers.Length; i++)
      bodyRenderers[i].color = bodyFlashColor;

    yield return new WaitForSeconds(flashDuration);

    // Reset head
    if (headMaterial != null)
      headMaterial.SetFloat("_FlashAmount", 0f);

    // Reset body colors
    for (int i = 0; i < bodyRenderers.Length; i++)
      bodyRenderers[i].color = originalBodyColors[i];
  }
}
