using UnityEngine;

public class PlayerShooting : MonoBehaviour {
  public ObjectPool bulletPool;
  public Transform firePoint;
  private GameManager gameManager;

  void Start() {
    gameManager = GameManager.Instance;
  }

  void Update() {
    if (Input.GetMouseButtonDown(0) && !gameManager.IsGamePaused()) // left click
    {
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
}
