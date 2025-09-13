using UnityEngine;
using UnityEngine.EventSystems;

public class PrintAllEventSystem : MonoBehaviour {
  void Start() {
    EventSystem[] eventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);

    Debug.Log($"Found {eventSystems.Length} EventSystems in scene.");

    for (int i = 0; i < eventSystems.Length; i++) {
      Debug.Log($"EventSystem {i + 1}: {eventSystems[i].gameObject.name}", eventSystems[i].gameObject);
    }
  }
}
