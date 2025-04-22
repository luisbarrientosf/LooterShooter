using UnityEngine;

public class CoinMagnet : MonoBehaviour {
  public float magnetRange = 3f;
  public float magnetSpeed = 5f;
  private bool isMagnetized = false;
  private Transform player;

  void Update() {
    if (player == null) return;

    float distance = Vector2.Distance(transform.position, player.position);

    if (distance < magnetRange) {
      isMagnetized = true;
    }

    if (isMagnetized) {
      // Optionally rotate/spin
      transform.Rotate(0, 0, 180 * Time.deltaTime);

      // Move toward player
      transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * magnetSpeed);

      if (distance < 0.3f) {
        // Add coin and destroy self
        player.GetComponent<PlayerInventory>().AddCoin();
        Destroy(gameObject);
      }
    }
  }

}
