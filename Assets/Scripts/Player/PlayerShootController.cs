using UnityEngine;

public class PlayerShooting : MonoBehaviour {
  public ObjectPool bulletPool;
  public Transform firePoint;
  private GameManager gameManager;
  private IInputProvider inputProvider;

  void Start() {
    gameManager = GameManager.Instance;
    inputProvider = GetComponent<IInputProvider>();
    CheckPlayerShooting();
  }

  void Update() {
    if (!CheckPlayerShooting()) return;
    if (inputProvider.IsAttackPressed() && !gameManager.IsGamePaused()) {
      Shoot();
    }
  }

  void Shoot() {
    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

    Bullet bullet = bulletPool.Get().GetComponent<Bullet>();
    bullet.transform.position = firePoint.position;
    bullet.Init(direction, bulletPool);
  }

  bool CheckPlayerShooting() {
    bool isValid = true;
    if (bulletPool == null) {
      Debug.LogError("Bullet pool is not assigned.");
      isValid = false;
    }

    if (firePoint == null) {
      Debug.LogError("Fire point is not assigned.");
      isValid = false;
    }

    if (gameManager == null) {
      Debug.LogError("GameManager instance is missing.");
      isValid = false;
    }

    if (inputProvider == null) {
      Debug.LogError("IInputProvider component is missing.");
      isValid = false;
    }

    return isValid;
  }
}
