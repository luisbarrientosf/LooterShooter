using UnityEngine;

/// <summary>
/// Base class for MonoBehaviour singletons.
/// Inherit like: public class GameManager : Singleton<GameManager> {}
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
  private static T _instance;
  public static T Instance {
    get {
      if (_instance == null) {
        // Try to find existing instance
        _instance = FindObjectOfType<T>();

        // If none found, optionally create one
        if (_instance == null) {
          GameObject go = new GameObject(typeof(T).Name);
          _instance = go.AddComponent<T>();
          DontDestroyOnLoad(go);
        }
      }
      return _instance;
    }
  }

  protected virtual void Awake() {
    if (_instance != null && _instance != this) {
      Destroy(gameObject);
      return;
    }

    _instance = this as T;
    DontDestroyOnLoad(gameObject);
  }
}
