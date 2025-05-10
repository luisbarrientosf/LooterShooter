using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimations : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
  public RectTransform textTransform;
  private Vector2 originalPosition;

  void Start() {
    if (textTransform != null)
      originalPosition = textTransform.anchoredPosition;
  }

  public void OnPointerDown(PointerEventData eventData) {
    if (textTransform != null)
      textTransform.anchoredPosition = originalPosition + new Vector2(0, -4);
  }

  public void OnPointerUp(PointerEventData eventData) {
    if (textTransform != null)
      textTransform.anchoredPosition = originalPosition;

    EventSystem.current.SetSelectedGameObject(null);
  }
}
